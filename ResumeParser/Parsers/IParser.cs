using ResumeParser.Model;
using ResumeParser.Model.Models;

namespace ResumeParser.Parsers
{
    public interface IParser
    {
        void Parse(Section section, Resume resume);
    }
}
