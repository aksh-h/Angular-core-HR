using ResumeParser.Model;
using ResumeParser.Model.Models;

namespace ResumeParser.Parsers
{
    public class SummaryParser : IParser
    {
        public void Parse(Section section, Resume resume)
        {
            resume.SummaryDescription = string.Join("; ", section.Content);
        }
    }
}
