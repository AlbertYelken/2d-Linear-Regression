List<Tuple<float, float>> themeParkNumberOfMascotsAndGuests = new List<Tuple<float, float>>()
{
    new Tuple<float, float>(0, 100),
    new Tuple<float, float>(2, 300),
    new Tuple<float, float>(4, 500),
};
// Prints 600

Console.WriteLine(TwoDimensionalLinearRegressionModel.Predict(themeParkNumberOfMascotsAndGuests, 5f));

public static class TwoDimensionalLinearRegressionModel
{ 
    public static float Predict(List<Tuple<float, float>> data, float inputXValue)
    {
        // y=mx+b
        var summedData = GetSumsOfXAndY(data);
        float initialAverageX = summedData.Item1/data.Count;
        float initialAverageY = summedData.Item2/data.Count;
        
        var seriesOfXResultTimesYResult = GetSeriesForRegression(data, initialAverageX, initialAverageY);
        
        float slopeM = seriesOfXResultTimesYResult.Item1/seriesOfXResultTimesYResult.Item2;
        float yInterceptB = initialAverageY - initialAverageX * slopeM;

        float yPrediction = slopeM * inputXValue + yInterceptB;
        
        return yPrediction;
    }

    private static Tuple<float, float> GetSumsOfXAndY(List<Tuple<float, float>> data)
    {
        var averageX = data.Sum(x => x.Item1);
        var averageY = data.Sum(x => x.Item2);
        
        return new Tuple<float, float>(averageX, averageY);
    }

    private static Tuple<float, float> GetSeriesForRegression(List<Tuple<float, float>> data, float initialAverageX, float initialAverageY)
    {
        var sumOfSquaresXAndY = 0f;
        var sumOfSquaresX = 0f;
        for (int i = data.Count - 1; i >= 0; i--)
        {
            sumOfSquaresXAndY += (data[i].Item2 - initialAverageY) * (data[i].Item1 - initialAverageX);

            sumOfSquaresX += (data[i].Item1-initialAverageX)*(data[i].Item1-initialAverageX);
        }
        
        return new Tuple<float, float>(sumOfSquaresXAndY, sumOfSquaresX);
    }
}