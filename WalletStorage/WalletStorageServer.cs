using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace PackageWallet.WalletStorage
{
    class WalletStorageServer:WalletStorageJson,IWalletStorage
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
                WalletItems walletItems = Deserialize(json);
                onGet?.Invoke(walletItems);
            }
        }
        
        
        private IEnumerator SaveToServer(string url, string json, Action<string> response)
        {
            var uwr = UnityWebRequest.Post(url,json);
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