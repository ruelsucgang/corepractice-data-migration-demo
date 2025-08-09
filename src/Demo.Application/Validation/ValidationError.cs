namespace Demo.Application.Validation;

public record ValidationError(int RowNumber, string Field, string Message);
