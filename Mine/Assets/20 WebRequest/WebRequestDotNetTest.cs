using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Http;

namespace Myproject
{
    public class WebRequestDotNetTest : MonoBehaviour
    {
        public string imageURL = "https://picsum.photos/1920/1080";

        //public Image image;

        public RawImage rawImage;

        private Coroutine coroutine;

        private void Start()
        {
            GetTexture();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GetTexture();
            }

            if(Input.GetKeyUp(KeyCode.Return))
            {
                if(coroutine == null)
                {
                    coroutine = StartCoroutine(GetTextureCoroutine());
                }
                else
                    StopCoroutine(coroutine);
            }
        }

        private async void GetTexture()
        {
            using (HttpClient httpClient = new())
            {
                // await Ű���尡 ������ ���⼭ ������ ��ȯ�� �� ���� �񵿱� ���·� ���.
                byte[] response = await httpClient.GetByteArrayAsync(imageURL);

                Texture2D texture = new(1, 1);

                texture.LoadImage(response);
                rawImage.texture = texture;


            }
        }

        IEnumerator GetTextureCoroutine()
        {
            while(true)
            {
                GetTexture();
                yield return new WaitForSeconds(10f);
            }
        }
    }
}
