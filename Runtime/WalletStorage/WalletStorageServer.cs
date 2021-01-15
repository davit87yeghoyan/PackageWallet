using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PackageWallet.Runtime.WalletStorage
{
    public class WalletStorageServer:WalletStorageJson,IWalletStorage
    {
        private readonly string _url;
        private readonly MonoBehaviour _mono;
        
        public WalletStorageServer(MonoBehaviour mono, string url)
        {
            _url = url;
            _mono = mono;
        }

        public void Save(WalletItems walletItems, Action onSave)
        {
            string json = Serialization(walletItems);
            _mono.StartCoroutine(SaveToServer(_url, json,Response));
            void Response(string response)
            {
                onSave?.Invoke();
            }
        }
        
        public void Get(Action<WalletItems> onGet)
        {
            _mono.StartCoroutine(GetFromServer(_url, Response));
            void Response(string json)
            {
                if(string.IsNullOrEmpty(json)) return;
                WalletItems walletItems = Deserialize(json);
                onGet?.Invoke(walletItems);
            }
        }
        
        
        private IEnumerator SaveToServer(string url, string json, Action<string> response)
        {
            byte[] bytePostData = Encoding.UTF8.GetBytes(json);
            var uwr = UnityWebRequest.Put(url,bytePostData);
            uwr.method = "POST"; //hack to send POST to server instead of PUT
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.SetRequestHeader("Accept", "application/json");
            return Request(uwr,response);
        }
        

        private IEnumerator GetFromServer(string url, Action<string> response)
        {
            var uwr = UnityWebRequest.Get(url);
            return Request(uwr,response);
        }
        
        
        private IEnumerator Request(UnityWebRequest uwr, Action<string> response)
        {
            yield return uwr.SendWebRequest();

            if (uwr.isHttpError || uwr.isNetworkError)
            {
                Debug.LogErrorFormat("error request [{0}, {1}]", uwr.url, uwr.error);
                uwr.Dispose();
                yield return null;
            }
            
            response(uwr.downloadHandler.text);        
            uwr.Dispose();
        }
        
        
    }
    
    
    
   


}