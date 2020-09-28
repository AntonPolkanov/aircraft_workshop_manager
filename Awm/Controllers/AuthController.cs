﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Awm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // GET
        [HttpGet("{userRole}")]
        public ActionResult<string> GetAccessToken(string userRole)
        {
            string result;
            switch (userRole)
            {
                case "admin":
                    result = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6InYwcFM1OEJyRVBrYjR6UjNHakFsbSJ9.eyJodHRwczovL2JhaWppdS9yb2xlcyI6WyJhZG1pbiJdLCJpc3MiOiJodHRwczovL2Rldi1hbnRvbi5hdS5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NWY3MTM5OTk3YzEyZDUwMDc5NjRhMzc5IiwiYXVkIjpbImh0dHBzOi8vYmFpaml1LXByb2QiLCJodHRwczovL2Rldi1hbnRvbi5hdS5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNjAxMjY2MjU5LCJleHAiOjE2MDEyNzM0NTksImF6cCI6IndLMFhyZDRLa05WWWhMc2o5SnRmNWNxVDNDak4zRG9MIiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCByZWFkOmFpcmNyYWZ0In0.gaLksZ8bGgrO5XxkTk5SLsGJ5L-DgvcZCR8UC7zHgg0ie-3D_Iu4pt_fmenXwGt8a3Cx0cwUykI5RygF5ENU3iJ4msnmuHK-QQKt2VtmEFGQDlZPOfgoQbNyWcHiVf3xKFIr9ya-BVocYA1dPvI7rRAp6BPeOZR4RVpQFXOvJ9c4UY7eHWb8XpwxTM2xf4euDUn64N9akRq0iNwQodUNTWbEP-haoFTlWpQUNqd41tRYFsqrKZNb3QHGRb-0kpGFb9MMkcMiOf4ljSqub5R136NUeKU4RlfU0U4cU1OiKgsAIbO6I3L-4OqO65CtID7VyX8056Cq4HoT61BWV58nmQ";
                    break;
                case "manager":
                    result = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6InYwcFM1OEJyRVBrYjR6UjNHakFsbSJ9.eyJodHRwczovL2JhaWppdS9yb2xlcyI6WyJtYW5hZ2VyIl0sImlzcyI6Imh0dHBzOi8vZGV2LWFudG9uLmF1LmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw1ZjZjNzEyMGZlMjE5MzAwNzYxODE1NWUiLCJhdWQiOlsiaHR0cHM6Ly9iYWlqaXUtcHJvZCIsImh0dHBzOi8vZGV2LWFudG9uLmF1LmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE2MDEyNjYzMTUsImV4cCI6MTYwMTI3MzUxNSwiYXpwIjoid0swWHJkNEtrTlZZaExzajlKdGY1Y3FUM0NqTjNEb0wiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIHJlYWQ6YWlyY3JhZnQifQ.Y1Q5JIsm5xSiYaoM3koqMJnXwToYf2woB5v_4etTZw1tdnhbWsUritJJIcuWR6E67g6LqyCV8iuXpu66jwg6Cd2AmUMugEesSBgp8N-fLdskf1LBWjip7jnV8w7GCfgJ8O77EYrATGiesDSpAEJkwxy2dRUBKlslNcW61kMGIXrY-7yX9Tx7L68B87t7Ycm_YSd2Pewf8QRcEhn1JaO3H3C1L4arbt70i6TjpIxDz6lsjKN3ZHIxLWpZIRdBFSPELpy1JDl139QeTbkC1ZOkARQHuPiXjgcmCoUbDbBCScYfqTsdS0YwUCE6xBbi4p1pYoggiv3b5b8QZwvUt0irQw";
                    break;
                case "worker":
                    result = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6InYwcFM1OEJyRVBrYjR6UjNHakFsbSJ9.eyJodHRwczovL2JhaWppdS9yb2xlcyI6WyJ3b3JrZXIiXSwiaXNzIjoiaHR0cHM6Ly9kZXYtYW50b24uYXUuYXV0aDAuY29tLyIsInN1YiI6ImF1dGgwfDVmNmM3MTRhYzU4YzFkMDA3MjkyNDMxNiIsImF1ZCI6WyJodHRwczovL2JhaWppdS1wcm9kIiwiaHR0cHM6Ly9kZXYtYW50b24uYXUuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTYwMTI2NjQxMiwiZXhwIjoxNjAxMjczNjEyLCJhenAiOiJ3SzBYcmQ0S2tOVlloTHNqOUp0ZjVjcVQzQ2pOM0RvTCIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwgcmVhZDphaXJjcmFmdCJ9.cEGWFCcwWQlu0Uu9nKOIllA8kUmCag2jT4vgwLOuUiifF9S5ub16oyb6QRkSdM_2Ikw4DHjxfeQWPRugNPwv_yqfWHFThSj32Er_uXQvDjnoWsDQv6g-LMAMKwmqRQqbrh4_UgpYuNBNGigN3JT1xYcutSJ0bI7fPuq1z210wlADFf2ASTepJFBraHVYpEZr1xfA51C-kFsd9BWerx7l2e5-n5_pKmUS_qjDEkZV_UTtCW9EVO_7gyZGOWmBd1bc0dH9yqkn4XAAeCw2tWGh-uDCN7Gg-9PbsPWINiaxQ-ahSPgv_ajl1R_bVvxyrKNB_20-_2DNWV2kvdjJnfndJA";
                    break;
                default: // client
                    result = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6InYwcFM1OEJyRVBrYjR6UjNHakFsbSJ9.eyJodHRwczovL2JhaWppdS9yb2xlcyI6WyJjbGllbnQiXSwiaXNzIjoiaHR0cHM6Ly9kZXYtYW50b24uYXUuYXV0aDAuY29tLyIsInN1YiI6ImF1dGgwfDVmNzE2MjE1OGU4OTI1MDA2ZmYyYjQyNSIsImF1ZCI6WyJodHRwczovL2JhaWppdS1wcm9kIiwiaHR0cHM6Ly9kZXYtYW50b24uYXUuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTYwMTI2NjQ2NCwiZXhwIjoxNjAxMjczNjY0LCJhenAiOiJ3SzBYcmQ0S2tOVlloTHNqOUp0ZjVjcVQzQ2pOM0RvTCIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwgcmVhZDphaXJjcmFmdCJ9.PHBb4KXtDOwUfZ4UUUznGUjBWADGPQWcU64a85tCB27fw-6hyBIZvtixjXGFWfu5CQvwbzyKhdNqh3zZ45lEZbqaBehkeU7bulC8JijIihuPi7qPyI87fDUDURYhp-LJOhBavDa-IMs4DSYkq-rx4O-l9vQhcMm0roXxmAokfKYMARCax6-E-C1mQZlXD3cMn_XP4GWT8R-ZXsDu_-sJwAYfCzOkj6Y1zWTZdr7_geYKPEPrIZZS7fuX04NJngdiUIXx_rjW5Y41A-2Inr4Y5dJO4qIax4tdkpGa0Zut0CM8osQ98X1TlsI4Sg8odTsBlxwduAs6dTxBA6muHIDDRw";
                    break;
            }

            return result;
        }
    }
}