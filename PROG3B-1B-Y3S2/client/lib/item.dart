import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class Item extends StatefulWidget {
  Item({
    Key? key,
    required this.image,
    required this.ddc,
    required this.title,
  }) : super(key: key);
  String ddc, title, image;
  @override
  State<Item> createState() => _ItemState();
}

class _ItemState extends State<Item> {
  @override
  Widget build(BuildContext context) {
    var height = MediaQuery.of(context).size.height;
    var width = MediaQuery.of(context).size.width;

    return Center(
      child: Container(
          width: width * 0.6,
          height: height * 0.3,
          child: Card(
            elevation: 5,
            child: Row(
              children: [
                Expanded(
                    child: SizedBox(child: Image.network("${widget.image}"))),
                SizedBox(
                  width: 15,
                ),
                Expanded(
                    child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      "Name",
                      style:
                          TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                    SizedBox(
                      height: 20,
                    ),
                    Text(
                      widget.title,
                      style: TextStyle(fontSize: 28),
                      textAlign: TextAlign.center,
                    ),
                  ],
                )),
                SizedBox(
                  width: 10,
                ),
                Expanded(
                    child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      "Classification",
                      style:
                          TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                    SizedBox(
                      height: 20,
                    ),
                    Text(widget.ddc, style: TextStyle(fontSize: 30)),
                  ],
                ))
              ],
            ),
          )),
    );
  }
}
