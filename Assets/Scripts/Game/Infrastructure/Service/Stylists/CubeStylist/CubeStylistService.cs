public class CubeStylistService : ICubeStylistService {
    private MergenCubeStylesData _stylesData;

    public CubeStylistService (MergenCubeStylesData stylesData) {
        _stylesData = stylesData;
    }

    public CubeStyleData GetStyle (int index) {
        return _stylesData.GetStyle(index);
    }
}