using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Kritik.Shared.Models;

public class Evaluation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string EvaluatorId { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProjectId { get; set; } = null!;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public RubricScores Scores { get; set; } = new();

    public string? Feedback { get; set; }
    public string? EvidenceUrl { get; set; }
}

public class RubricScores
{
    [Range(1, 5, ErrorMessage = "Score must be between 1 and 5")]
    public int Innovation { get; set; }

    [Range(1, 5, ErrorMessage = "Score must be between 1 and 5")]
    public int Usability { get; set; }

    [Range(1, 5, ErrorMessage = "Score must be between 1 and 5")]
    public int TechnicalComplexity { get; set; }

    public double Average => (Innovation + Usability + TechnicalComplexity) / 3.0;
}
