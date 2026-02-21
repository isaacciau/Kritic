// lib/models/project_model.dart
import 'package:json_annotation/json_annotation.dart';

part 'project_model.g.dart';

@JsonSerializable()
class Project {
  final String? id;
  final String? title;
  final String? status;
  final List<String>? techStack;
  final String? iconUrl;

  Project({this.id, this.title, this.status, this.techStack, this.iconUrl});

  factory Project.fromJson(Map<String, dynamic> json) =>
      _$ProjectFromJson(json);
  Map<String, dynamic> toJson() => _$ProjectToJson(this);
}
