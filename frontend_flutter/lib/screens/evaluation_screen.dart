// lib/screens/evaluation_screen.dart
import 'dart:io';
import 'package:flutter/material.dart';
import '../theme/app_theme.dart';
import '../widgets/custom_evaluation_slider.dart';

class EvaluationScreen extends StatefulWidget {
  final String projectName;

  const EvaluationScreen({super.key, required this.projectName});

  @override
  State<EvaluationScreen> createState() => _EvaluationScreenState();
}

class _EvaluationScreenState extends State<EvaluationScreen> {
  double _score = 5.0;
  String?
  _evidencePath; // TODO: [MAUI Migration] Hook up image_picker for real photos

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          'Evaluación',
          style: Theme.of(context).textTheme.headlineMedium,
        ),
        backgroundColor: AppColors.backgroundOffWhite,
        elevation: 0,
        iconTheme: const IconThemeData(color: AppColors.textPrimary),
      ),
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(24.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Text(
                widget.projectName,
                style: Theme.of(context).textTheme.headlineLarge,
              ),
              const SizedBox(height: 32),
              const Text(
                'Calificación general',
                style: TextStyle(fontWeight: FontWeight.w600, fontSize: 16),
              ),
              const SizedBox(height: 16),
              CustomEvaluationSlider(
                value: _score,
                onChanged: (val) {
                  setState(() {
                    _score = val;
                  });
                },
              ),
              const SizedBox(height: 32),
              const Text(
                'Evidencia',
                style: TextStyle(fontWeight: FontWeight.w600, fontSize: 16),
              ),
              const SizedBox(height: 16),
              if (_evidencePath != null)
                Align(
                  alignment: Alignment.centerLeft,
                  child: Stack(
                    clipBehavior: Clip.none,
                    children: [
                      Container(
                        width: 100,
                        height: 100,
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          image: DecorationImage(
                            image: FileImage(File(_evidencePath!)),
                            fit: BoxFit.cover,
                          ),
                        ),
                      ),
                      Positioned(
                        right: -8,
                        top: -8,
                        child: GestureDetector(
                          onTap: () {
                            setState(() {
                              _evidencePath = null;
                            });
                          },
                          child: Container(
                            decoration: const BoxDecoration(
                              color: AppColors.errorRed,
                              shape: BoxShape.circle,
                            ),
                            padding: const EdgeInsets.all(4),
                            child: const Icon(
                              Icons.close,
                              color: Colors.white,
                              size: 16,
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                )
              else
                OutlinedButton.icon(
                  onPressed: () {
                    // TODO: [MAUI Migration] Implement image capture using image_picker
                    // Mocking an image selection
                    setState(() {
                      _evidencePath = 'mock_path_to_image';
                    });
                  },
                  icon: const Icon(
                    Icons.camera_alt_outlined,
                    color: AppColors.textPrimary,
                  ),
                  label: const Text(
                    'Tomar Foto de Evidencia',
                    style: TextStyle(color: AppColors.textPrimary),
                  ),
                  style: OutlinedButton.styleFrom(
                    padding: const EdgeInsets.symmetric(vertical: 16),
                    side: const BorderSide(color: AppColors.borderColor),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8),
                    ),
                  ),
                ),
              const SizedBox(height: 32),
              const Text(
                'Comentarios',
                style: TextStyle(fontWeight: FontWeight.w600, fontSize: 16),
              ),
              const SizedBox(height: 16),
              TextFormField(
                maxLines: 4,
                decoration: const InputDecoration(
                  hintText: 'Añade observaciones sobre el proyecto...',
                ),
              ),
              const SizedBox(height: 48),
              ElevatedButton.icon(
                onPressed: () {
                  // TODO: [MAUI Migration] Call API Service submitEvaluation method
                  Navigator.pop(context);
                },
                icon: const Icon(Icons.send, color: AppColors.textPrimary),
                label: const Text('Enviar Evaluación'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
