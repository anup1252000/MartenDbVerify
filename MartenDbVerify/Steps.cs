namespace MartenDbVerify
{
    public record Step1Completed(string data);
    public record Step2Completed(string data);
    public record Step3Completed(string data);
    public record Step4Completed(string data);
    public record CompleteStep1Command(string Data);
    public record CompleteStep2Command(string Data);
    public record CompleteStep3Command(string Data);
    public record CompleteStep4Command(string Data);
}
