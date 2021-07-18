using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace yu_gi_oh
{
    public class OAuthInterceptor : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {

            request.Headers.Add("Authorization", "Bearer" + Constants.TOKEN_CONSTANT);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
