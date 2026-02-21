namespace PRH.Shared.DataTransferObjets.RequestFeatures;

public class PagedListDto<T>
{
    public PagedListDto()
    {
    }

    public List<T> Items { get; set; }
    public MetaDataDto MetaData { get; set; }
}
