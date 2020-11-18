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
        static readonly string indianStateCodeHeaders = "SrNo,State Name,TIN,StateCode";
        static readonly string indianStateCensusFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\IndiaStateCensusData - IndiaStateCensusData - IndiaStateCensusData - IndiaStateCensusData.csv";
        static readonly string wrongHeaderIndianCensusFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\WrongIndiaStateCensusData - WrongIndiaStateCensusData - WrongIndiaStateCensusData - WrongIndiaStateCensusData.csv";
        static readonly string delimiterIndianCensusFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\DelimiterIndiaStateCensusData - DelimiterIndiaStateCensusData - DelimiterIndiaStateCensusData - DelimiterIndiaStateCensusD.csv";
        static readonly string wrongIndianStateCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\IndiaData.csv";
        static readonly string wrongIndianStateCensusFileType = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CsvFiles\IndiaStateCensusData.txt";
        static readonly string indianStateCodeFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\IndiaStateCode.csv";
        static readonly string wrongIndianStateCodeFileType = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\IndianStateCode.txt";
        static readonly string delimiterIndianStateCodeFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\DelimiterIndiaStateCode - DelimiterIndiaStateCode - DelimiterIndiaStateCode - DelimiterIndiaStateCode.csv";
        static readonly string wrongHeaderStateCodeFilePath = @"E:\Studies\Bridgelabz\Fellowship--.NET\Project's\Indian State Census Analyzer\IndianStateCensusAnalyzerTest\CSVfiles\WrongIndiaStateCode - WrongIndiaStateCode - WrongIndiaStateCode - WrongIndiaStateCode.csv";

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
            stateRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCodeFilePath, indianStateCodeHeaders);
            Assert.AreEqual(29, totalRecord.Count);
            Assert.AreEqual(37, stateRecord.Count);
        }
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }
        [Test]
        public void GivenWrongIndianCensusDataFileType_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFileType, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCodeFileType, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, stateException.eType);
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianCensusFilePath, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianStateCodeFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, stateException.eType);
        }

        [Test]
        public void GivenIndianCensusDataFile_WhenHeaderNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderIndianCensusFilePath, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderStateCodeFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, stateException.eType);
        }
    }
}