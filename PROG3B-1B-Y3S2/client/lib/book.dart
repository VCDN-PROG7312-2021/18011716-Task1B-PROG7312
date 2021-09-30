class Book {
  late String image;
  late String ddc;
  late String title;

  Book() {}

  get getImage {
    return image;
  }

  get getTitle {
    return image;
  }

  get getDDC {
    return image;
  }

  set setImage(String url) {
    image = url;
  }

  set setDDC(String code) {
    ddc = code;
  }

  set setTitle(String name) {
    title = name;
  }

  Book.fromJson(Map<String, dynamic> json) {
    image = json['image'];
    ddc = json['ddc'];
    title = json['title'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['image'] = this.image;
    data['ddc'] = this.ddc;
    data['title'] = this.title;
    return data;
  }
}
