using System.Collections;
using System.Collections.Generic;
using EshopPOC.API;
using UnityEngine;
using UnityEngine.Networking;

namespace EshopPOC
{
    public class UserManager : MonoBehaviour
    {
        public static User User
        {
            get => _user;
            private set
            {
                _user = value;
                OnUserChange?.Invoke(value);
            }
        }

        private static User _user;

        public delegate void OnUserChangeDelegate(User newUser);

        public static event OnUserChangeDelegate OnUserChange;

        public void Login(string email, string password)
        {
            StartCoroutine(LoginCoroutine(email, password));
        }

        public void Register(string email, string password)
        {
            StartCoroutine(RegisterCoroutine(email, password));
        }

        private IEnumerator LoginCoroutine(string email, string password)
        {
            var loginReq = EshopAPI.Post("/users/login",
                new Dictionary<string, object>
            {
                { "email", email },
                { "password", password }
            });
            
            yield return loginReq.SendWebRequest();

            var loginRes = EshopAPI.ParseRequest<LoginInformation>(loginReq);

            if (!loginRes.success)
            {
                Debug.LogError(loginRes.error);
                yield break;
            }

            var getUserReq = EshopAPI.Get($"/users/{loginRes.result.id}");
            
            EshopAPI.AuthorizeRequest(getUserReq, loginRes.result.token);
            
            yield return getUserReq.SendWebRequest();

            var userRes = EshopAPI.ParseRequest<User>(getUserReq);

            if (!userRes.success)
            {
                Debug.LogError(userRes.error);
                yield break;
            }

            User = userRes.result;
            User.token = loginRes.result.token;
        }

        private IEnumerator RegisterCoroutine(string email, string password)
        {
            var registerReq = EshopAPI.Post("/users/register",
                new Dictionary<string, object>
            {
                { "name", GetNameFromEmail(email) },
                { "email", email },
                { "password", password }
            });

            yield return registerReq.SendWebRequest();
            
            var registerRes = EshopAPI.ParseRequest<User>(registerReq);

            if (!registerRes.success)
            {
                Debug.LogError(registerRes.error);
                yield break;
            }

            yield return LoginCoroutine(email, password);
        }

        private string GetNameFromEmail(string email)
        {
            if (!email.Contains("@"))
                return email;

            var leftPart = email.Split('@')[0];

            if (!leftPart.Contains("."))
                return leftPart;

            var nameParts = leftPart.Split('.');

            for (int i = 0; i < nameParts.Length; i++)
                nameParts[i] = nameParts[i][0].ToString().ToUpper() + nameParts[i].Substring(1);

            return string.Join(" ", nameParts);
        }
    }
}