using Microsoft.VisualStudio.TestTools.UnitTesting;
using HaikuDetectorCS;
using System.IO;

namespace HaikuDetectorCS.Tests
{
    [TestClass]
    public class HaikuDetectorTests
    {
        private readonly HaikuDetector haikuDetector;

        public HaikuDetectorTests()
        {
            haikuDetector = new HaikuDetector();
        }

        [TestMethod]
        public void ShouldReadTextFilesFromDirectory()
        {
            var numberOfTextFiles = Directory.GetFiles(haikuDetector.DefaultDirectoryPath, "*.txt").Length;
            var numberOfHaikuMessages = haikuDetector.ReadAllFromDirectory(null).Length;
            Assert.AreEqual(numberOfTextFiles, numberOfHaikuMessages);
        }

        [TestMethod]
        public void ShouldNotHaveTooFewLines()
        {
            var numberOfLines = File.ReadAllLines($"{haikuDetector.DefaultDirectoryPath}/PoemWithTooManyLines.txt");
            Assert.AreNotEqual(numberOfLines, HaikuDetector.NumberOfLinesInHaiku);
        }

        [TestMethod]
        public void ShouldNotHaveTooManyLines()
        {
            var numberOfLines = File.ReadAllLines($"{haikuDetector.DefaultDirectoryPath}/PoemWithTooFewLines.txt");
            Assert.AreNotEqual(numberOfLines, HaikuDetector.NumberOfLinesInHaiku);
        }

        [TestMethod]
        public void ShouldGetInvalidMessageWhenHaikuHasIncorrectSyllablePattern()
        {
            var message = haikuDetector.ReadFromFile($"{haikuDetector.DefaultDirectoryPath}/PoemWithIncorrectSyllablePattern.txt");
            Assert.AreEqual(message, $"Not a haiku poem because {HaikuDetector.HaikuRules.IncorrectNumberOfSyllables} {1}.");
        }

        [TestMethod]
        public void ShouldGetInvalidMessageWhenHaikuHasTooManySyllables()
        {
            var message = haikuDetector.ReadFromFile($"{haikuDetector.DefaultDirectoryPath}/PoemWithTooManySyllables.txt");
            Assert.AreEqual(message, $"Not a haiku poem because {HaikuDetector.HaikuRules.IncorrectNumberOfSyllables} {2}.");
        }

        [TestMethod]
        public void ShouldGetInvalidMessageWhenHaikuHasTooFewSyllables()
        {
            var message = haikuDetector.ReadFromFile($"{haikuDetector.DefaultDirectoryPath}/PoemWithTooFewSyllables.txt");
            Assert.AreEqual(message, $"Not a haiku poem because {HaikuDetector.HaikuRules.IncorrectNumberOfSyllables} {3}.");
        }
    }
}
