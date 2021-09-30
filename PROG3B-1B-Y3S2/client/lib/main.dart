import 'dart:convert';
import 'package:flash/flash.dart';

import 'package:audioplayers/audioplayers.dart';
import 'package:client/item.dart';
import 'package:flutter/material.dart';
import 'package:reorderables/reorderables.dart';
import 'package:http/http.dart' as http;

import 'book.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key? key, required this.title}) : super(key: key);
  final String title;
  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int score = 0;
  AudioPlayer audioPlayer = AudioPlayer();
  late AudioCache audioCache;
  String path = "bgMusic.wav";

  late List<Widget> _rows = [];
  List<Book> books = [];
  bool loader = false;
  late Future<List<Book>> _ftr;
  @override
  void initState() {
    _ftr = getItems();
    print("<---------------------- EXTRACTING BOOK ---------------------->");
    extractBooks().then((value) {
      print(value);
      setState(() {
        books = value;
      });
    });
    super.initState();
  }

  Future<List<Book>> extractBooks() async {
    late List<Book> books = [];
    for (int i = 0; i < 10; i++) {
      var uri = "http://localhost:51059/api/values/$i";
      var res = await http.get(Uri.parse(uri)).then((value) {
        books.add(Book.fromJson(jsonDecode(value.body)));
      });
    }

    if (books.length > 0) return books;
    return books;
  }

  @override
  Widget build(BuildContext context) {
    ScrollController _scrollController =
        PrimaryScrollController.of(context) ?? ScrollController();

    void _onReorder(int oldIndex, int newIndex) {
      Book currentBook = books[oldIndex];
      books.removeAt(oldIndex);
      books.insert(newIndex, currentBook);

      setState(() {
        Widget row = _rows.removeAt(oldIndex);
        _rows.insert(newIndex, row);
      });
    }

    return Scaffold(
      appBar: AppBar(
        title: Text("PROG3B Task 1 - B"),
        actions: [
          Row(
            children: [
              Text(
                "Score",
                style: TextStyle(fontSize: 24),
              ),
              SizedBox(
                width: 15,
              ),
              Text("${score}", style: TextStyle(fontSize: 24)),
              SizedBox(
                width: 10,
              )
            ],
          ),
        ],
      ),
      body: Container(
          height: MediaQuery.of(context).size.height,
          child: FutureBuilder<List<Book>>(
              future: _ftr,
              builder: (context, snapshot) {
                return snapshot.hasData
                    ? SizedBox(
                        height: MediaQuery.of(context).size.height,
                        child: CustomScrollView(
                          controller: _scrollController,
                          slivers: <Widget>[
                            ReorderableSliverList(
                              delegate:
                                  ReorderableSliverChildListDelegate(_rows),
                              onReorder: _onReorder,
                            )
                          ],
                        ),
                      )
                    : Center(
                        child: CircularProgressIndicator(),
                      );
              })),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          List<String> ddc = [];
          for (Book i in books) {
            ddc.add(i.ddc);
          }
          var uri = "http://localhost:51059/api/values/save";
          var res = await http.post(Uri.parse(uri),
              body: jsonEncode(ddc),
              headers: {"Content-Type": "application/json"});
          print("Response --> ${res.body}");
          if (res.body == "true") {
            context.showSuccessBar(content: Text("Awesome! Keep going..."));
            setState(() {
              score += 25;
            });
          } else {
            context.showErrorBar(content: Text("Oops. Keep trying... :)"));
            if (score >= 10) {
              setState(() {
                score -= 10;
              });
            } else {
              setState(() {
                score = 0;
              });
            }
          }
        },
        tooltip: 'Check',
        child: const Icon(Icons.check),
      ),
    );
  }

  Future<List<Book>> getItems() async {
    List<Book> allBooks = [];
    extractBooks().then((value) {
      allBooks = value;
      print("Preparing list items");
      for (int i = 0; i < allBooks.length; i++) {
        _rows.add(Item(
          ddc: "${allBooks[i].ddc}",
          title: "${allBooks[i].title}",
          image: "${allBooks[i].image}",
        ));
      }
      print("All books retrieved!");
      return allBooks;
    });
    return allBooks;
  }
}
