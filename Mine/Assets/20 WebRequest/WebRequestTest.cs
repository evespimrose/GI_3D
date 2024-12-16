using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Myproject
{
    public class WebRequestTest : MonoBehaviour
    {
        public string imageURL = "https://picsum.photos/1920/1080";

        //public Image image;

        public RawImage rawImage;

        private void Start()
        {
            _ = StartCoroutine(GetVebTexture(imageURL));
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                _ = StartCoroutine(GetVebTexture(imageURL));
            }
        }

        private IEnumerator GetVebTexture(string url)
        {
            while (true)
            {
                // http로 웹 요청(Request)을 보낼 객체 생성
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

                // 코루틴을 통해 웹으로부터 응답(Response)을 받을 때 까지 비동기로 대기하는 객체를 받아옴
                UnityWebRequestAsyncOperation operation = www.SendWebRequest();

                yield return operation;

                if (www.result != UnityWebRequest.Result.Success)
                    Debug.Log($"Http 통신 실패! : {www.error}");
                else
                {
                    Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    rawImage.texture = texture;
                    //Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height),
                    //    new Vector2(0.5f, 0.5f));
                    //image.sprite = sprite;
                }

                yield return new WaitForSeconds(5f);
            }

            
        }
    }

}
