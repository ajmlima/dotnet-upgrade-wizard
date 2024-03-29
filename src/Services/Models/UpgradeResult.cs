namespace WizardTool.Services.Models;

public record UpgradeResult(bool Success, int NumberSucceeded = 0, int NumberFailed = 0, int NumberSkipped = 0);