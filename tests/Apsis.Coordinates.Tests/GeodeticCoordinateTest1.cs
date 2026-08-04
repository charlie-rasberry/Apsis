namespace Apsis.Coordinates.Tests;

public class GeodeticCoordinateTest1
{
    // source: https://geodesy.noaa.gov
    private readonly string JsonString = """
                                {
                                        "ID": "1785608631077",
                                        "nadconVersion": "5.0",
                                        "vertconVersion": "3.0",
                                        "srcDatum": "NAD83(1986)",
                                        "destDatum": "NAD83(2011)",
                                        "srcVertDatum": "N/A",
                                        "destVertDatum": "N/A",
                                        "srcLat": "40.0000000000",
                                        "srcLatDms": "N400000.00000",
                                        "destLat": "39.9999983008",
                                        "destLatDms": "N395959.99388",
                                        "deltaLat": "-0.189",
                                        "sigLat": "0.000263",
                                        "sigLat_m": "0.0081",
                                        "srcLon": "-80.0000000000",
                                        "srcLonDms": "W0800000.00000",
                                        "destLon": "-79.9999976143",
                                        "destLonDms": "W0795959.99141",
                                        "deltaLon": "0.204",
                                        "sigLon": "0.000221",
                                        "sigLon_m": "0.0052",
                                        "heightUnits": "m",
                                        "srcEht": "100.000",
                                        "destEht": "N/A",
                                        "sigEht": "N/A",
                                        "srcOrthoht": "N/A",
                                        "destOrthoht": "N/A",
                                        "sigOrthoht": "N/A",
                                        "spcZone": "PA S-3702",
                                        "spcNorthing_m": "76,470.391",
                                        "spcEasting_m": "407,886.681",
                                        "spcNorthing_usft": "250,886.607",
                                        "spcEasting_usft": "1,338,208.220",
                                        "spcNorthing_ift": "250,887.109",
                                        "spcEasting_ift": "1,338,210.896",
                                        "spcConvergence": "-01 27 35.22",
                                        "spcScaleFactor": "0.99999024",
                                        "spcCombinedFactor": "N/A",
                                        "utmZone": "UTM Zone 17",
                                        "utmNorthing": "4,428,235.878",
                                        "utmEasting": "585,360.668",
                                        "utmConvergence": "00 38 34.18",
                                        "utmScaleFactor": "0.99968970",
                                        "utmCombinedFactor": "N/A",
                                        "x": "N/A",
                                        "y": "N/A",
                                        "z": "N/A",
                                        "usng": "17SNE8536128236"
                                }
                                """;
}
