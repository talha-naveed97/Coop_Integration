using Models;

namespace API.Facebook
{
    public interface IFacebookApiService
    {
        IEnumerable<LeadsAdModel> GetLeadsAdByAdGroup(string adGroupId);

        IEnumerable<LeadsAdModel> GetLeadsAdByForm(string formId);
    }
}
