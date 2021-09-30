using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class AwakeScreenEffect : MonoBehaviour
{
    public Shader shader;
    [Range(0f, 1f)]
    [Tooltip("���ѽ���")]
    public float progress;
    [Range(0, 4)]
    [Tooltip("ģ����������")]
    public int blurIterations = 0;
    [Range(.2f, 3f)]
    [Tooltip("ÿ��ģ������ʱ��ģ����С��ɢ")]
    public float blurSpread = .6f;

    public Flowchart flowchart;
    public float blinkTime = 0;
    public float blinkTimeMax = 2;
    public bool Opening = true;


    [SerializeField]
    Material material;
    Material Material
    {
        get
        {
            if (material == null)
            {
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
            }
            return material;
        }
    }

    void OnDisable()
    {
        if (material)
        {
            DestroyImmediate(material);
        }
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Material.SetFloat("_Progress", progress);
        if (progress < 1)
        {
            // ���ڽ�������Ӱ��ģ���������������ԣ�����û��ʹ��
            int rtW = src.width;
            int rtH = src.height;
            var buffer0 = RenderTexture.GetTemporary(rtW, rtH, 0);
            buffer0.filterMode = FilterMode.Bilinear;
            Graphics.Blit(src, buffer0, Material, 0);   // ��ƤPass
                                                        // ģ��
            float blurSize;
            for (int i = 0; i < blurIterations; i++)
            {
                // ��progress(0~1)ӳ�䵽blurSize(blurSize~0)
                blurSize = 1f + i * blurSpread;
                blurSize = blurSize - blurSize * progress;
                Material.SetFloat("_BlurSize", blurSize);
                // ��ֱ�����Pass
                var buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 1);
                RenderTexture.ReleaseTemporary(buffer0);
                // ��ֱ�����Pass
                buffer0 = buffer1;
                buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 2);

                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
            }
            Graphics.Blit(buffer0, dest);
            RenderTexture.ReleaseTemporary(buffer0);
        }
        else
        {
            // ��ȫ���������账��ֱ��blit
            Graphics.Blit(src, dest);
        }
    }

    void Update()
    {
        if (flowchart.GetBooleanVariable("EyeBlink"))
        {
            
            if(progress< 1&& Opening)
            {
                blinkTime += Time.deltaTime;
                progress = blinkTime / blinkTimeMax;
                Debug.Log("time+");
                if(progress>=1)
                {
                    Opening = false;
                }
            }
            else if(!Opening)
            {
                blinkTime -= Time.deltaTime;
                progress = blinkTime / blinkTimeMax;
                Debug.Log("time-");
            }
            if(blinkTime < 0)
            {
                Debug.Log("time stop");
                flowchart.SetBooleanVariable("EyeBlink", false);
                blinkTime = 0;
                Opening = true;
                progress = 0;
            }          
        }
        else if(flowchart.GetBooleanVariable("KeepOpen"))
        {

            if (progress < 1 && Opening)
            {
                blinkTime += Time.deltaTime;
                progress = blinkTime / blinkTimeMax;
                Debug.Log("time+");
                if (progress >= 1)
                {
                    flowchart.SetBooleanVariable("KeepOpen", false);
                    progress = 1;
                    blinkTime = blinkTimeMax;
                    
                }
            }
        }
        if (flowchart.GetBooleanVariable("KeepClosing"))
        {

            if (progress >0 && Opening)
            {
                blinkTime -= Time.deltaTime;
                progress = blinkTime / blinkTimeMax;
                Debug.Log("time+");
                if (progress <= 0)
                {
                    flowchart.SetBooleanVariable("KeepClosing", false);
                    progress = 0;
                    blinkTime = 0;                
                }
            }
        }
    }
}
