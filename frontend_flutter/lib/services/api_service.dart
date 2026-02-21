// lib/services/api_service.dart
import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';
import '../models/user_model.dart';
import '../models/project_model.dart';
import '../models/evaluation_model.dart';

// TODO: [MAUI Migration] Replace with actual .NET backend URL
const String _baseUrl = 'https://api.kritik.com/v1';

class ApiService {
  final Dio _dio;

  ApiService()
    : _dio = Dio(
        BaseOptions(
          baseUrl: _baseUrl,
          connectTimeout: const Duration(seconds: 10),
          receiveTimeout: const Duration(seconds: 10),
        ),
      );

  Future<User?> login(String id) async {
    try {
      final response = await _dio.post(
        '/auth/login',
        data: {'institutionalId': id},
      );
      if (response.statusCode == 200) {
        return User.fromJson(response.data);
      }
      return null;
    } on DioException catch (e) {
      // TODO: [MAUI Migration] Implement specific error handling based on existing C# exceptions
      debugPrint('Login error: ${e.message}');
      throw Exception('Failed to login: ${e.message}');
    }
  }

  Future<List<Project>> getProjects() async {
    try {
      final response = await _dio.get('/projects');
      if (response.statusCode == 200) {
        final List<dynamic> data = response.data;
        return data.map((json) => Project.fromJson(json)).toList();
      }
      return [];
    } on DioException catch (e) {
      debugPrint('Get projects error: ${e.message}');
      throw Exception('Failed to load projects: ${e.message}');
    }
  }

  Future<bool> submitEvaluation(Evaluation data) async {
    try {
      // TODO: [MAUI Migration] Handle multipart/form-data if evidencePhotoPath is included
      final response = await _dio.post('/evaluations', data: data.toJson());
      return response.statusCode == 200 || response.statusCode == 201;
    } on DioException catch (e) {
      debugPrint('Submit evaluation error: ${e.message}');
      throw Exception('Failed to submit evaluation: ${e.message}');
    }
  }
}
