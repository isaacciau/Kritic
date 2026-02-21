// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'project_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Project _$ProjectFromJson(Map<String, dynamic> json) => Project(
  id: json['id'] as String?,
  title: json['title'] as String?,
  status: json['status'] as String?,
  techStack: (json['techStack'] as List<dynamic>?)
      ?.map((e) => e as String)
      .toList(),
  iconUrl: json['iconUrl'] as String?,
);

Map<String, dynamic> _$ProjectToJson(Project instance) => <String, dynamic>{
  'id': instance.id,
  'title': instance.title,
  'status': instance.status,
  'techStack': instance.techStack,
  'iconUrl': instance.iconUrl,
};
