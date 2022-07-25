using System.Collections.Generic;

namespace Ardalis.HttpClientTestExtensions.Api.Dtos;

public class ListResponse<T>
{
  public List<T> Data { get; private set; } = new();
  public int TotalCount { get; private set; } = 0;
  public int PageNumber { get; private set; } = 0;

  public ListResponse(List<T> data)
  {
    Data = data;
    TotalCount = Data.Count;
    PageNumber = 1;
  }

  public ListResponse(List<T> data, int totalCount = 0, int pageNumber = 1)
  {
    Data = data;
    TotalCount = totalCount;
    PageNumber = pageNumber;
  }
}
