using AnalysisTool.Models;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace AnalysisTool
{
    public class RateTables
    {
        public List<string> uspsPSFCRowIndex = new List<string>();
        public List<string> uspsPSCubicRowIndex = new List<string>();
        public List<string> uspsPSPMRowIndex = new List<string>();
        public List<string> uspsPSPMERowIndex = new List<string>();
        public List<string> uspsPSPSRowIndex = new List<string>();
        public List<string> uspsPSFCPIRowIndex = new List<string>();
        public List<string> uspsPSPMIRowIndex = new List<string>();
        public List<string> uspsPSPMEIRowIndex = new List<string>();
        //public List<string> uspsPSPMEFRRowIndex = new List<string>();
        //public List<string> uspsPSFRRowIndex = new List<string>();
        //public List<string> uspsPSRRRowIndex = new List<string>();
        //public List<string> uspsPSPMIFRRowIndex = new List<string>();
        //public List<string> uspsPSPMEIFRRowIndex = new List<string>();

        public List<string> uspsPPFCRowIndex = new List<string>();
        public List<string> uspsPPCubicRowIndex = new List<string>();
        public List<string> uspsPPPMRowIndex = new List<string>();
        public List<string> uspsPPPMERowIndex = new List<string>();
        public List<string> uspsPPPSRowIndex = new List<string>();
        public List<string> uspsPPFCPIRowIndex = new List<string>();
        public List<string> uspsPPPMIRowIndex = new List<string>();
        public List<string> uspsPPPMEIRowIndex = new List<string>();
        //public List<string> uspsPPPMEFRRowIndex = new List<string>();
        //public List<string> uspsPPFRRowIndex = new List<string>();
        //public List<string> uspsPPRRRowIndex = new List<string>();
        //public List<string> uspsPPPMIFRRowIndex = new List<string>();
        //public List<string> uspsPPPMEIFRRowIndex = new List<string>();

        public List<string> uspsCSFCRowIndex = new List<string>();
        public List<string> uspsCSCubicRowIndex = new List<string>();
        public List<string> uspsCSPMRowIndex = new List<string>();
        public List<string> uspsCSPMERowIndex = new List<string>();
        public List<string> uspsCSPSRowIndex = new List<string>();
        public List<string> uspsCSFCPIRowIndex = new List<string>();
        public List<string> uspsCSPMIRowIndex = new List<string>();
        public List<string> uspsCSPMEIRowIndex = new List<string>();
        //public List<string> uspsCSPMEFRRowIndex = new List<string>();
        //public List<string> uspsCSFRRowIndex = new List<string>();
        //public List<string> uspsCSPMIFRRowIndex = new List<string>();
        //public List<string> uspsCSPMEIFRRowIndex = new List<string>();

        public List<string> upsPNDARowIndex = new();
        public List<string> upsPNDASRowIndex = new();
        public List<string> upsPSDARowIndex = new();
        public List<string> upsPTDSRowIndex = new();
        public List<string> upsPGroundRowIndex = new();

        public List<string> upsCNDARowIndex = new();
        public List<string> upsCNDASRowIndex = new();
        public List<string> upsCSDARowIndex = new();
        public List<string> upsCTDSRowIndex = new();
        public List<string> upsCGroundRowIndex = new();

        public List<string> fedexPPORowIndex = new();
        public List<string> fedexPSORowIndex = new();
        public List<string> fedexPTDARowIndex = new();
        public List<string> fedexPTDRowIndex = new();
        public List<string> fedexPESRowIndex = new();
        public List<string> fedexPGroundRowIndex = new();
        public List<string> fedexPGHRowIndex = new();

        public List<string> fedexCPORowIndex = new();
        public List<string> fedexCSORowIndex = new();
        public List<string> fedexCTDARowIndex = new();
        public List<string> fedexCTDRowIndex = new();
        public List<string> fedexCESRowIndex = new();
        public List<string> fedexCGroundRowIndex = new();
        public List<string> fedexCGHRowIndex = new();

        public List<string> uspsIntZoneRowIndex = new List<string>();

        //public string rateSheetsLocation = "C:\\Users\\Administrator\\Desktop\\RateSheets";
        public string rateSheetsLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RateSheets";

        public string uspsPSCubic = "/uspsPSCubic.csv";
        public string uspsPSFC = "/uspsPSFC.csv";
        public string uspsPSPM = "/uspsPSPM.csv";
        public string uspsPSPME = "/uspsPSPME.csv";
        public string uspsPSPS = "/uspsPSPS.csv";
        public string uspsPSFCPI = "/uspsPSFCPI.csv";
        public string uspsPSPMI = "/uspsPSPMI.csv";
        public string uspsPSPMEI = "/uspsPSPMEI.csv";
        //public string uspsPSPMEFR = "/uspsPSPMEFR.csv";
        //public string uspsPSFR = "/uspsPSFR.csv";
        //public string uspsPSRR = "/uspsPSRR.csv";
        //public string uspsPSPMIFR = "/uspsPSPMIFR.csv";
        //public string uspsPSPMEIFR = "/uspsPSPMEIFR.csv";

        public string uspsPPCubic = "/uspsPPCubic.csv";
        public string uspsPPFC = "/uspsPPFC.csv";
        public string uspsPPPM = "/uspsPPPM.csv";
        public string uspsPPPME = "/uspsPPPME.csv";
        public string uspsPPPS = "/uspsPPPS.csv";
        public string uspsPPFCPI = "/uspsPPFCPI.csv";
        public string uspsPPPMI = "/uspsPPPMI.csv";
        public string uspsPPPMEI = "/uspsPPPMEI.csv";
        //public string uspsPPPMEFR = "/uspsPPPMEFR.csv";
        //public string uspsPPFR = "/uspsPPFR.csv";
        //public string uspsPPRR = "/uspsPPRR.csv";
        //public string uspsPPPMIFR = "/uspsPPPMIFR.csv";
        //public string uspsPPPMEIFR = "/uspsPPPMEIFR.csv";

        public string uspsCSCubic = "/uspsCSCubic.csv";
        public string uspsCSFC = "/uspsCSFC.csv";
        public string uspsCSPM = "/uspsCSPM.csv";
        public string uspsCSPME = "/uspsCSPME.csv";
        public string uspsCSPS = "/uspsCSPS.csv";
        public string uspsCSFCPI = "/uspsCSFCPI.csv";
        public string uspsCSPMI = "/uspsCSPMI.csv";
        public string uspsCSPMEI = "/uspsCSPMEI.csv";
        //public string uspsCSPMEFR = "/uspsCSPMEFR.csv";
        //public string uspsCSFR = "/uspsCSFR.csv";
        //public string uspsCSPMIFR = "/uspsCSPMIFR.csv";
        //public string uspsCSPMEIFR = "/uspsCSPMEIFR.csv";

        public string upsPNDA = "/upsPNDA.csv";
        public string upsPNDAS = "/upsPNDAS.csv";
        public string upsPSDA = "/upsPSDA.csv";
        public string upsPTDS = "/upsPTDS.csv";
        public string upsPGround = "/upsPGround.csv";

        public string upsCNDA = "/upsCNDA.csv";
        public string upsCNDAS = "/upsCNDAS.csv";
        public string upsCSDA = "/upsCSDA.csv";
        public string upsCTDS = "/upsCTDS.csv";
        public string upsCGround = "/upsCGround.csv";

        public string fedexPPO = "/fedexPPO.csv";
        public string fedexPSO = "/fedexPSO.csv";
        public string fedexPTDA = "/fedexPTDA.csv";
        public string fedexPTD = "/fedexPTD.csv";
        public string fedexPES = "/fedexPES.csv";
        public string fedexPGround = "/fedexPGround.csv";
        public string fedexPGH = "/fedexPGroundH.csv";

        public string fedexCPO = "/fedexCPO.csv";
        public string fedexCSO = "/fedexCSO.csv";
        public string fedexCTDA = "/fedexCTDA.csv";
        public string fedexCTD = "/fedexCTD.csv";
        public string fedexCES = "/fedexCES.csv";
        public string fedexCGround = "/fedexCGround.csv";
        public string fedexCGH = "/fedexCGroundH.csv";

        public string uspsIntZoneLookup = "/USPS country code to zone.csv";

        public DataTable uspsPSFCdt = new DataTable();
        public DataTable uspsPSCubicdt = new DataTable();
        public DataTable uspsPSPMdt = new DataTable();
        public DataTable uspsPSPMEdt = new DataTable();
        //public DataTable uspsPSPMEFRdt = new DataTable();
        public DataTable uspsPSPSdt = new DataTable();
        //public DataTable uspsPSFRdt = new DataTable();
        //public DataTable uspsPSRRdt = new DataTable();
        public DataTable uspsPSPMIdt = new DataTable();
        //public DataTable uspsPSPMIFRdt = new DataTable();
        public DataTable uspsPSFCPIdt = new DataTable();
        public DataTable uspsPSPMEIdt = new DataTable();
        //public DataTable uspsPSPMEIFRdt = new DataTable();

        public DataTable uspsPPFCdt = new DataTable();
        public DataTable uspsPPCubicdt = new DataTable();
        public DataTable uspsPPPMdt = new DataTable();
        public DataTable uspsPPPMEdt = new DataTable();
        //public DataTable uspsPPPMEFRdt = new DataTable();
        public DataTable uspsPPPSdt = new DataTable();
        //public DataTable uspsPPFRdt = new DataTable();
        //public DataTable uspsPPRRdt = new DataTable();
        public DataTable uspsPPPMIdt = new DataTable();
        //public DataTable uspsPPPMIFRdt = new DataTable();
        public DataTable uspsPPFCPIdt = new DataTable();
        public DataTable uspsPPPMEIdt = new DataTable();
        //public DataTable uspsPPPMEIFRdt = new DataTable();

        public DataTable uspsCSFCdt = new DataTable();
        public DataTable uspsCSCubicdt = new DataTable();
        public DataTable uspsCSPMdt = new DataTable();
        public DataTable uspsCSPMEdt = new DataTable();
        //public DataTable uspsCSPMEFRdt = new DataTable();
        public DataTable uspsCSPSdt = new DataTable();
        //public DataTable uspsCSFRdt = new DataTable();
        public DataTable uspsCSPMIdt = new DataTable();
        //public DataTable uspsCSPMIFRdt = new DataTable();
        public DataTable uspsCSFCPIdt = new DataTable();
        public DataTable uspsCSPMEIdt = new DataTable();
        //public DataTable uspsCSPMEIFRdt = new DataTable();

        public DataTable upsPNDAdt = new();
        public DataTable upsPNDASdt = new();
        public DataTable upsPSDAdt = new();
        public DataTable upsPTDSdt = new();
        public DataTable upsPGrounddt = new();

        public DataTable upsCNDAdt = new();
        public DataTable upsCNDASdt = new();
        public DataTable upsCSDAdt = new();
        public DataTable upsCTDSdt = new();
        public DataTable upsCGrounddt = new();

        public DataTable fedexPPOdt = new();
        public DataTable fedexPSOdt = new();
        public DataTable fedexPTDAdt = new();
        public DataTable fedexPTDdt = new();
        public DataTable fedexPESdt = new();
        public DataTable fedexPGrounddt = new();
        public DataTable fedexPGHdt = new();

        public DataTable fedexCPOdt = new();
        public DataTable fedexCSOdt = new();
        public DataTable fedexCTDAdt = new();
        public DataTable fedexCTDdt = new();
        public DataTable fedexCESdt = new();
        public DataTable fedexCGrounddt = new();
        public DataTable fedexCGHdt = new();

        public DataTable uspsIntZones = new DataTable();

        // consider turning these into dictionaries instead of lists => Dictionary<rateName, RateSheetsModel>
        // then in the rate calculation reference the rates by rateName rather than index
        // that way we don't have to keep track of the index of each rate
        public List<RateSheetsModel> ps = new List<RateSheetsModel>();
        public List<RateSheetsModel> pp = new List<RateSheetsModel>();
        public List<RateSheetsModel> cs = new List<RateSheetsModel>();
        public List<RateSheetsModel> cp = new();

        public DateTime startPS = new DateTime(2022, 1, 10);
        public DateTime startPP = new DateTime(2022, 10, 3);
        public DateTime endPP = new DateTime(2022, 12, 26);
        public DateTime startCS = new DateTime(2023, 1, 22);
        public DateTime startCP = new DateTime(2023, 10, 3);
        public DateTime endCP = new DateTime(2023, 12, 26);

        public void ProcessRateTables(FormDataModel formData)
        {
            Console.WriteLine(rateSheetsLocation);

            RateSheetsModel fcPS = new RateSheetsModel(uspsPSFCdt, uspsPSFCRowIndex, uspsPSFC);
            RateSheetsModel cubicPS = new RateSheetsModel(uspsPSCubicdt, uspsPSCubicRowIndex, uspsPSCubic);
            RateSheetsModel pmPS = new RateSheetsModel(uspsPSPMdt, uspsPSPMRowIndex, uspsPSPM);
            RateSheetsModel pmePS = new RateSheetsModel(uspsPSPMEdt, uspsPSPMERowIndex, uspsPSPME);
            //RateSheetsModel pmefrPS = new RateSheetsModel(uspsPSPMEFRdt, uspsPSPMEFRRowIndex, uspsPSPMEFR);
            RateSheetsModel psPS = new RateSheetsModel(uspsPSPSdt, uspsPSPSRowIndex, uspsPSPS);
            //RateSheetsModel frPS = new RateSheetsModel(uspsPSFRdt, uspsPSFRRowIndex, uspsPSFR);
            //RateSheetsModel rrPS = new RateSheetsModel(uspsPSRRdt, uspsPSRRRowIndex, uspsPSRR);
            RateSheetsModel fcpiPS = new RateSheetsModel(uspsPSFCPIdt, uspsPSFCPIRowIndex, uspsPSFCPI);
            RateSheetsModel pmiPS = new RateSheetsModel(uspsPSPMIdt, uspsPSPMIRowIndex, uspsPSPMI);
            //RateSheetsModel pmifrPS = new RateSheetsModel(uspsPSPMIFRdt, uspsPSPMIFRRowIndex, uspsPSPMIFR);
            RateSheetsModel pmeiPS = new RateSheetsModel(uspsPSPMEIdt, uspsPSPMEIRowIndex, uspsPSPMEI);
            //RateSheetsModel pmeifrPS = new RateSheetsModel(uspsPSPMEIFRdt, uspsPSPMEIFRRowIndex, uspsPSPMEIFR);

            RateSheetsModel fcPP = new RateSheetsModel(uspsPPFCdt, uspsPPFCRowIndex, uspsPPFC);
            RateSheetsModel cubicPP = new RateSheetsModel(uspsPPCubicdt, uspsPPCubicRowIndex, uspsPPCubic);
            RateSheetsModel pmPP = new RateSheetsModel(uspsPPPMdt, uspsPPPMRowIndex, uspsPPPM);
            RateSheetsModel pmePP = new RateSheetsModel(uspsPPPMEdt, uspsPPPMERowIndex, uspsPPPME);
            //RateSheetsModel pmefrPP = new RateSheetsModel(uspsPPPMEFRdt, uspsPPPMEFRRowIndex, uspsPPPMEFR);
            RateSheetsModel psPP = new RateSheetsModel(uspsPPPSdt, uspsPPPSRowIndex, uspsPPPS);
            //RateSheetsModel frPP = new RateSheetsModel(uspsPPFRdt, uspsPPFRRowIndex, uspsPPFR);
            //RateSheetsModel rrPP = new RateSheetsModel(uspsPPRRdt, uspsPPRRRowIndex, uspsPPRR);
            RateSheetsModel fcpiPP = new RateSheetsModel(uspsPPFCPIdt, uspsPPFCPIRowIndex, uspsPPFCPI);
            RateSheetsModel pmiPP = new RateSheetsModel(uspsPPPMIdt, uspsPPPMIRowIndex, uspsPPPMI);
            //RateSheetsModel pmifrPP = new RateSheetsModel(uspsPPPMIFRdt, uspsPPPMIFRRowIndex, uspsPPPMIFR);
            RateSheetsModel pmeiPP = new RateSheetsModel(uspsPPPMEIdt, uspsPPPMEIRowIndex, uspsPPPMEI);
            //RateSheetsModel pmeifrPP = new RateSheetsModel(uspsPPPMEIFRdt, uspsPPPMEIFRRowIndex, uspsPPPMEIFR);

            RateSheetsModel fcCS = new RateSheetsModel(uspsCSFCdt, uspsCSFCRowIndex, uspsCSFC);
            RateSheetsModel cubicCS = new RateSheetsModel(uspsCSCubicdt, uspsCSCubicRowIndex, uspsCSCubic);
            RateSheetsModel pmCS = new RateSheetsModel(uspsCSPMdt, uspsCSPMRowIndex, uspsCSPM);
            RateSheetsModel pmeCS = new RateSheetsModel(uspsCSPMEdt, uspsCSPMERowIndex, uspsCSPME);
            //RateSheetsModel pmefrCS = new RateSheetsModel(uspsCSPMEFRdt, uspsCSPMEFRRowIndex, uspsCSPMEFR);
            RateSheetsModel psCS = new RateSheetsModel(uspsCSPSdt, uspsCSPSRowIndex, uspsCSPS);
            //RateSheetsModel frCS = new RateSheetsModel(uspsCSFRdt, uspsCSFRRowIndex, uspsCSFR);
            RateSheetsModel fcpiCS = new RateSheetsModel(uspsCSFCPIdt, uspsCSFCPIRowIndex, uspsCSFCPI);
            RateSheetsModel pmiCS = new RateSheetsModel(uspsCSPMIdt, uspsCSPMIRowIndex, uspsCSPMI);
            //RateSheetsModel pmifrCS = new RateSheetsModel(uspsCSPMIFRdt, uspsCSPMIFRRowIndex, uspsCSPMIFR);
            RateSheetsModel pmeiCS = new RateSheetsModel(uspsCSPMEIdt, uspsCSPMEIRowIndex, uspsCSPMEI);
            //RateSheetsModel pmeifrCS = new RateSheetsModel(uspsCSPMEIFRdt, uspsCSPMEIFRRowIndex, uspsCSPMEIFR);

            RateSheetsModel uPNDA = new RateSheetsModel(upsPNDAdt, upsPNDARowIndex, upsPNDA);
            RateSheetsModel uPNDAS = new RateSheetsModel(upsPNDASdt, upsPNDASRowIndex, upsPNDAS);
            RateSheetsModel uPSDA = new RateSheetsModel(upsPSDAdt, upsPSDARowIndex, upsPSDA);
            RateSheetsModel uPTDS = new RateSheetsModel(upsPTDSdt, upsPTDSRowIndex, upsPTDS);
            RateSheetsModel uPGround = new RateSheetsModel(upsPGrounddt, upsPGroundRowIndex, upsPGround);

            RateSheetsModel uCNDA = new RateSheetsModel(upsCNDAdt, upsCNDARowIndex, upsCNDA);
            RateSheetsModel uCNDAS = new RateSheetsModel(upsCNDASdt, upsCNDASRowIndex, upsCNDAS);
            RateSheetsModel uCSDA = new RateSheetsModel(upsCSDAdt, upsCSDARowIndex, upsCSDA);
            RateSheetsModel uCTDS = new RateSheetsModel(upsCTDSdt, upsCTDSRowIndex, upsCTDS);
            RateSheetsModel uCGround = new RateSheetsModel(upsCGrounddt, upsCGroundRowIndex, upsCGround);

            RateSheetsModel fPPO = new RateSheetsModel(fedexPPOdt, fedexPPORowIndex, fedexPPO);
            RateSheetsModel fPSO = new RateSheetsModel(fedexPSOdt, fedexPSORowIndex, fedexPSO);
            RateSheetsModel fPTDA = new RateSheetsModel(fedexPTDAdt, fedexPTDARowIndex, fedexPTDA);
            RateSheetsModel fPTD = new RateSheetsModel(fedexPTDdt, fedexPTDRowIndex, fedexPTD);
            RateSheetsModel fPES = new RateSheetsModel(fedexPESdt, fedexPESRowIndex, fedexPES);
            RateSheetsModel fPGround = new RateSheetsModel(fedexPGrounddt, fedexPGroundRowIndex, fedexPGround);
            RateSheetsModel fPGH = new RateSheetsModel(fedexPGHdt, fedexPGHRowIndex, fedexPGH);

            RateSheetsModel fCPO = new RateSheetsModel(fedexCPOdt, fedexCPORowIndex, fedexCPO);
            RateSheetsModel fCSO = new RateSheetsModel(fedexCSOdt, fedexCSORowIndex, fedexCSO);
            RateSheetsModel fCTDA = new RateSheetsModel(fedexCTDAdt, fedexCTDARowIndex, fedexCTDA);
            RateSheetsModel fCTD = new RateSheetsModel(fedexCTDdt, fedexCTDRowIndex, fedexCTD);
            RateSheetsModel fCES = new RateSheetsModel(fedexCESdt, fedexCESRowIndex, fedexCES);
            RateSheetsModel fCGround = new RateSheetsModel(fedexCGrounddt, fedexCGroundRowIndex, fedexCGround);
            RateSheetsModel fCGH = new RateSheetsModel(fedexCGHdt, fedexCGHRowIndex, fedexCGH);

            ps.Add(fcPS);
            ps.Add(cubicPS);
            ps.Add(pmPS);
            ps.Add(pmePS);
            ps.Add(psPS);
            ps.Add(fcpiPS);
            ps.Add(pmiPS);
            ps.Add(pmeiPS);
            ps.Add(uPNDA);
            ps.Add(uPNDAS);
            ps.Add(uPSDA);
            ps.Add(uPTDS);
            ps.Add(uPGround);
            ps.Add(fPPO);
            ps.Add(fPSO);
            ps.Add(fPTDA);
            ps.Add(fPTD);
            ps.Add(fPES);
            ps.Add(fPGround);
            ps.Add(fPGH);
            //ps.Add(pmefrPS);
            //ps.Add(frPS);
            //ps.Add(rrPS);
            //ps.Add(pmifrPS);
            //ps.Add(pmeifrPS);

            pp.Add(fcPP);
            pp.Add(cubicPP);
            pp.Add(pmPP);
            pp.Add(pmePP);
            pp.Add(psPP);
            pp.Add(fcpiPP);
            pp.Add(pmiPP);
            pp.Add(pmeiPP);
            pp.Add(uPNDA);
            pp.Add(uPNDAS);
            pp.Add(uPSDA);
            pp.Add(uPTDS);
            pp.Add(uPGround);
            ps.Add(fPPO);
            ps.Add(fPSO);
            ps.Add(fPTDA);
            ps.Add(fPTD);
            ps.Add(fPES);
            ps.Add(fPGround);
            ps.Add(fPGH);
            //pp.Add(pmefrPP);
            //pp.Add(frPP);
            //pp.Add(rrPP);
            //pp.Add(pmifrPP);
            //pp.Add(pmeifrPP);

            cs.Add(fcCS);
            cs.Add(cubicCS);
            cs.Add(pmCS);
            cs.Add(pmeCS);
            cs.Add(psCS);
            cs.Add(fcpiCS);
            cs.Add(pmiCS);
            cs.Add(pmeiCS);
            cs.Add(uCNDA);
            cs.Add(uCNDAS);
            cs.Add(uCSDA);
            cs.Add(uCTDS);
            cs.Add(uCGround);
            cs.Add(fCPO);
            cs.Add(fCSO);
            cs.Add(fCTDA);
            cs.Add(fCTD);
            cs.Add(fCES);
            cs.Add(fCGround);
            cs.Add(fCGH);
            //cs.Add(pmefrCS);
            //cs.Add(frCS);
            //cs.Add(pmifrCS);
            //cs.Add(pmeifrCS);

            cp.Add(fcCS);
            cp.Add(cubicCS);
            cp.Add(pmCS);
            cp.Add(pmeCS);
            cp.Add(psCS);
            cp.Add(fcpiCS);
            cp.Add(pmiCS);
            cp.Add(pmeiCS);
            cp.Add(uCNDA);
            cp.Add(uCNDAS);
            cp.Add(uCSDA);
            cp.Add(uCTDS);
            cp.Add(uCGround);
            cp.Add(fCPO);
            cp.Add(fCSO);
            cp.Add(fCTDA);
            cp.Add(fCTD);
            cp.Add(fCES);
            cp.Add(fCGround);
            cp.Add(fCGH);

            for (var x = 0; x < ps.Count; x++)
            {
                using (CsvReader csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(rateSheetsLocation + ps[x].rateSheetName)), true))
                {
                    ps[x].dataTable.Load(csvReader);

                    var j = 0;
                    foreach (var row in ps[x].dataTable.Rows)
                    {
                        ps[x].rowIndex.Add(ps[x].dataTable.Rows[j][0].ToString());
                        j++;
                    }
                }
            }

            for (var x = 0; x < pp.Count; x++)
            {
                using (CsvReader csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(rateSheetsLocation + pp[x].rateSheetName)), true))
                {
                    pp[x].dataTable.Load(csvReader);

                    var j = 0;
                    foreach (var row in pp[x].dataTable.Rows)
                    {
                        pp[x].rowIndex.Add(pp[x].dataTable.Rows[j][0].ToString());
                        j++;
                    }
                }
            }

            for (var x = 0; x < cs.Count; x++)
            {
                using (CsvReader csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(rateSheetsLocation + cs[x].rateSheetName)), true))
                {
                    cs[x].dataTable.Load(csvReader);

                    var j = 0;
                    foreach (var row in cs[x].dataTable.Rows)
                    {
                        cs[x].rowIndex.Add(cs[x].dataTable.Rows[j][0].ToString());
                        j++;
                    }
                }
            }

            for (var x = 0; x < cp.Count; x++)
            {
                using (CsvReader csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(rateSheetsLocation + cp[x].rateSheetName)), true))
                {
                    cp[x].dataTable.Load(csvReader);

                    var j = 0;
                    foreach (var row in cp[x].dataTable.Rows)
                    {
                        cp[x].rowIndex.Add(cp[x].dataTable.Rows[j][0].ToString());
                        j++;
                    }
                }
            }

            using (CsvReader csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(rateSheetsLocation + uspsIntZoneLookup)), true))
            {
                uspsIntZones.Load(csvReader);

                var j = 0;
                foreach (var row in uspsIntZones.Rows)
                {
                    uspsIntZoneRowIndex.Add(uspsIntZones.Rows[j][0].ToString());
                    j++;
                }
            }
        }

        public List<RateSheetsModel> DetermineRateTables(DateTime packageDate)
        {
            if (packageDate >= startPS && packageDate < startCS)
            {
                if (packageDate >= startPP && packageDate < endPP)
                {
                    return pp;
                }
                else return ps;
            }
            else
            {
                if (packageDate >= startCP && packageDate < endCP)
                {
                    return cp;
                }
                else return cs;
            }
        }
    }
}
