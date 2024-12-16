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
                // http�� �� ��û(Request)�� ���� ��ü ����
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

                // �ڷ�ƾ�� ���� �����κ��� ����(Response)�� ���� �� ���� �񵿱�� ����ϴ� ��ü�� �޾ƿ�
                UnityWebRequestAsyncOperation operation = www.SendWebRequest();

                yield return operation;

                if (www.result != UnityWebRequest.Result.Success)
                    Debug.Log($"Http ��� ����! : {www.error}");
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
