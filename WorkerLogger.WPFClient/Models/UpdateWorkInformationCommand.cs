using System;

namespace WorkerLogger.WPFClient.Models;

public record UpdateWorkInformationCommand(Guid Id, string Title, string? Description, TimeSpan TimeSpent);

