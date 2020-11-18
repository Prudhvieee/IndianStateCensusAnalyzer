using IndianStateCensusAnalyzer;
using IndianStateCensusAnalyzer.POCO;
using IndianStateCensusAnalyzer.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using static IndianStateCensusAnalyzer.CensusAnalyser;
namespace IndianStateCensusAnalyzerTest
{
    public class CensusAnalyserTest
    {
        static readonly string indianStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static readonly string indianStateCensusFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\IndianStateCensusAnalyzer\IndianStateCensusAnalyzerTest\CSVfiles\IndiaStateCensusData - IndiaStateCensusData - IndiaStateCensusData - IndiaStateCensusData.csv";
        static readonly string wrongHeaderIndianCensusFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\IndianStateCensusAnalyzer\IndianStateCensusAnalyzerTest\CSVfiles\WrongIndiaStateCensusData - WrongIndiaStateCensusData - WrongIndiaStateCensusData - WrongIndiaStateCensusData.csv";
        static readonly string delimiterIndianCensusFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\IndianStateCensusAnalyzer\IndianStateCensusAnalyzerTest\CSVfiles\DelimiterIndiaStateCensusData - DelimiterIndiaStateCensusData - DelimiterIndiaStateCensusData - DelimiterIndiaStateCensusD.csv";
        static readonly string wrongIndianStateCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\IndiaData.csv";
        static readonly string wrongIndianStateCensusFileType = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\IndianStateCensusAnalyzer\IndianStateCensusAnalyzerTest\CsvFiles\IndiaStateCensusData.txt";

        IndianStateCensusAnalyzer.CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;
        [SetUp]
        public void Setup()
        {
            censusAnalyser = new IndianStateCensusAnalyzer.CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCensusFilePath, indianStateCensusHeaders);
            Assert.AreEqual(29, totalRecord.Count);
        }
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.eType);
        }
        [Test]
        public void GivenWrongIndianCensusDataFileType_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFileType, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.eType);
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianCensusFilePath, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.eType);
        }

        [Test]
        public void GivenIndianCensusDataFile_WhenHeaderNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderIndianCensusFilePath, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.eType);
        }
    }
}