namespace CleanCodeTemplate.Business.Modules.Constants;

public class PatternConstants
{
    public const string Guid =
        "(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})";

    public const string Text = "^[\\w ]+$";
}