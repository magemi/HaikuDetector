#include "HaikuDetector.h"
#include <fstream>
#include <sstream>
#include <regex>

const int HaikuDetector::HAIKU_SYLLABLES_PER_LINE[] = { 5,7,5 };

std::vector<std::string> HaikuDetector::splitLineIntoWords(std::string line)
{
    std::vector<std::string> words;
    std::istringstream iss(line);
    std::string word;

    while (iss >> word)
        words.push_back(word);

    return words;
}

int HaikuDetector::getNumberOfSyllablesInLine(std::string line)
{
    int numberOfSyllables = 0;
    std::regex regEx("[aeiouy]+", std::regex_constants::icase);

    std::vector<std::string> words = splitLineIntoWords(line);

    for (std::string word : words)
    {
        auto wordStart = std::sregex_iterator(word.begin(), word.end(), regEx);
        auto wordEnd = std::sregex_iterator();
        int numberOfSyllablesInWord = std::distance(wordStart, wordEnd);

        if (word.back() == 'e' && numberOfSyllablesInWord > 1)
            numberOfSyllablesInWord--;

        numberOfSyllables += numberOfSyllablesInWord;
    }
    
    return numberOfSyllables;
}

std::string HaikuDetector::readFromFile(std::string fileName)
{
    std::ifstream file(fileName);
    int numberOfLines = 0;
    std::string message = "Valid haiku";

    if (file.is_open())
    {
        std::string line;
        while (std::getline(file, line))
        {
            if (getNumberOfSyllablesInLine(line) != HaikuDetector::HAIKU_SYLLABLES_PER_LINE[numberOfLines])
            {
                message = "Not a haiku poem because poem has the incorrect number of syllables in line " + std::to_string(numberOfLines + 1);
            }
            numberOfLines++;
        }
    }

    if (numberOfLines != HaikuDetector::NUMBER_OF_LINES_IN_HAIKU)
    {
        message = "Not a haiku poem because poem is not exactly three lines long";
    }

    return message;
}

std::vector<std::string> HaikuDetector::readAllFromDirectory(std::string path)
{
    std::vector<std::string> messages;

    for (const auto & entry : std::filesystem::directory_iterator(path))
    {
        std::string message = readFromFile(entry.path().string());
        messages.push_back(message);
    }

    return messages;
}