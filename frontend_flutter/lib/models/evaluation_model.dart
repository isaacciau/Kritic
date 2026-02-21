// lib/models/evaluation_model.dart
import 'package:json_annotation/json_annotation.dart';

part 'evaluation_model.g.dart';

@JsonSerializable()
class Evaluation {
  final String? projectId;
  final int? score;
  final String? comments;
  final String? evidencePhotoPath;

  Evaluation({
    this.projectId,
    this.score,
    this.comments,
    this.evidencePhotoPath,
  });

  factory Evaluation.fromJson(Map<String, dynamic> json) =>
      _$EvaluationFromJson(json);
  Map<String, dynamic> toJson() => _$EvaluationToJson(this);
}
