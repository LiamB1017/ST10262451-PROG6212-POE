public class Claim
{
    public int ClaimID { get; set; }
    public int LecturerID { get; set; }
    public double HoursWorked { get; set; }
    public double HourlyRate { get; set; }
    public double TotalAmount => HoursWorked * HourlyRate;
    public DateTime SubmissionDate { get; set; }
    public string Status { get; set; }
    public string SupportingDocumentPath { get; internal set; }
}
