using System;


namespace WorkerLogger.WPFClient.Models;

public record CreateWorkInformationCommand(string UserId, string Title, string? Description, TimeSpan TimeSpent);
