// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'evaluation_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Evaluation _$EvaluationFromJson(Map<String, dynamic> json) => Evaluation(
  projectId: json['projectId'] as String?,
  score: (json['score'] as num?)?.toInt(),
  comments: json['comments'] as String?,
  evidencePhotoPath: json['evidencePhotoPath'] as String?,
);

Map<String, dynamic> _$EvaluationToJson(Evaluation instance) =>
    <String, dynamic>{
      'projectId': instance.projectId,
      'score': instance.score,
      'comments': instance.comments,
      'evidencePhotoPath': instance.evidencePhotoPath,
    };
