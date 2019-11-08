using System;
using System.Globalization;

namespace ScheduledJob.Entities
{
    public class SerializeLotEff : ISerializeEff<LotEff>
    {
        public LotEff Deserialize(EffFile effFile)
        {
            LotEff lotEff = new LotEff();
            string oldQmpHeaderName = string.Empty;
            for (int i = 0; i < effFile.Header.Length; i++)
            {
                string headerName = effFile.Header[i];

                switch (headerName)
                {
                    case "Lot":
                        lotEff.Lot = effFile.LotData[i].Replace("\"", string.Empty);
                        break;

                    case "StopTimestamp":
                        lotEff.LohTimestamp = DateTime.Parse(effFile.LotData[i], CultureInfo.CreateSpecificCulture("de-DE"));
                        break;

                    case "ProcessGroup":
                        lotEff.ProcessGroup = effFile.LotData[i].Replace("\"", string.Empty);
                        break;

                    case "WaferCount":
                        lotEff.WaferCount = Convert.ToInt32(effFile.LotData[i].Replace("\"", string.Empty));
                        break;

                    case "BasicType":
                        lotEff.BasicType = effFile.LotData[i].Replace("\"", string.Empty);
                        break;

                    case "Product":
                        lotEff.Bau = effFile.LotData[i].Replace("\"", string.Empty);
                        break;

                    case "StopReason":
                        lotEff.SBA = effFile.LotData[i].Replace("\"", string.Empty);
                        break;
                    case string a when a.EndsWith("_Keyword"):
                    case string b when b.EndsWith("_Action"):
                    case "Equipment":
                        if (oldQmpHeaderName != headerName)
                        {
                            if (!string.IsNullOrWhiteSpace(oldQmpHeaderName))
                            {
                                lotEff.QMP = $"),{Environment.NewLine}";
                            }

                            lotEff.QMP = $"{headerName} (";
                            lotEff.QMP += effFile.LotData[i].Replace("\"", string.Empty);

                            oldQmpHeaderName = headerName;
                        }
                        else
                        {
                            lotEff.QMP += ";" + effFile.LotData[i].Replace("\"", string.Empty);
                        }

                        break;

                    default:
                        break;
                }

            }

            return lotEff;
        }

        public void Serialize(LotEff entity)
        {
            throw new NotImplementedException();
        }
    }
}
