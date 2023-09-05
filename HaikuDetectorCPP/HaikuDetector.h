#pragma once

#include <filesystem>
#include <string>
#include <vector>

class HaikuDetector
{
private:
    std::vector<std::string> splitLineIntoWords(std::string line);
    int getNumberOfSyllablesInLine(std::string line);
public:
    static const int NUMBER_OF_LINES_IN_HAIKU = 3;
    static const int HAIKU_SYLLABLES_PER_LINE[];

    std::string readFromFile(std::string file);
    std::vector<std::string> readAllFromDirectory(std::string path);

};

