using System;
using System.IO;
using ResumeParser.Model;
using ResumeParser.Model.Exceptions;
using ResumeParser;
using ResumeParser.Helpers;
using ResumeParser.Parsers;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace ResumeParser
{
    public class ResumeProcessor
    {
        private readonly IOutputFormatter _outputFormatter;
        private readonly IInputReader _inputReaders; 

        public ResumeProcessor()
        {
            //if (outputFormatter == null)
            //{
            //    throw new ArgumentNullException("outputFormatter");    
            //}
            //_outputFormatter = outputFormatter;
            IInputReaderFactory inputReaderFactory = new InputReaderFactory(new ConfigFileApplicationSettingsAdapter());
            _inputReaders = inputReaderFactory.LoadInputReaders();
        }        
        public string ProcessJson(string location)
        {
            try
            {
                var resume = GetResume(location);
                JsonSerializerSettings _settings = new JsonSerializerSettings();
                _settings.Converters.Add(new HyphenNameSerializer());
                _settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               var formatted = JsonConvert.SerializeObject(resume, Formatting.Indented, _settings);
                // var formatted = _outputFormatter.Format(resume);

                return formatted;
            }
            catch (IOException ex)
            {
                throw new ResumeParserException("There's a problem accessing the file, it might still being opened by other application", ex);
            }            
        }
        public Resume ProcessModel(string location)
        {
            try
            {
                var resume = GetResume(location);
                return resume;
            }
            catch (IOException ex)
            {
                throw new ResumeParserException("There's a problem accessing the file, it might still being opened by other application", ex);
            }
        }
        private Resume GetResume(string location)
        {
            var rawInput = _inputReaders.ReadIntoList(location);

            var sectionExtractor = new SectionExtractor();
            var sections = sectionExtractor.ExtractFrom(rawInput);

            IResourceLoader resourceLoader = new CachedResourceLoader(new ResourceLoader());
            var resumeBuilder = new ResumeBuilder(resourceLoader);
            var resume = resumeBuilder.Build(sections);
            return resume;
        }

        public string GetRawString(string location)
        {
            var rawInput = _inputReaders.ReadIntoList(location);
            StringBuilder builder = new StringBuilder();
            foreach(string line in rawInput)
            {
                builder.Append(line).Append(" ");
            }
            return builder.ToString();
        }
    }
}
