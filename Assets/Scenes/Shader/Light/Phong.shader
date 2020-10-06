Shader "Custom/Phong"{
    Properties{
        _Diffuse("Diffuse Color", Color) = (1, 1, 1, 1) 
        _Specular("Specular Color", Color) = (1, 1, 1, 1)
        _Gloss("Gloss", Range(8.0, 256)) = 20 // 高光的参数
    }
    SubShader{
        Pass {           
            // 只有定义了正确的LightMode才能得到一些Unity的内置光照变量
            Tags{"LightMode" = "ForwardBase"}

            CGPROGRAM

            // 包含unity的内置的文件，才可以使用Unity内置的一些变量
            #include "Lighting.cginc" 
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Diffuse;
            fixed4 _Specular;
            float _Gloss;

            struct a2v
            {
                float4 vertex : POSITION; // 告诉Unity把模型空间下的顶点坐标填充给vertex属性
                float3 normal : NORMAL; // 告诉Unity把模型空间下的法线方向填充给normal属性
            };

            struct v2f
            {
                float4 pos : SV_POSITION; // 声明用来存储顶点在裁剪空间下的坐标
                float3 worldNormal : TEXCOORD0; 
                float3 worldPos : TEXCOORD1;
            };

            // 计算顶点坐标从模型坐标系转换到裁剪面坐标系
            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); 
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject); 
                o.worldPos = mul(unity_WorldToObject, v.vertex).xyz; 

                return o;
            }

            // 计算每个像素点的颜色值
            fixed4 frag(v2f i) : SV_Target 
            {
                // 环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                // 法线方向
                fixed3 worldNormal = normalize(i.worldNormal); 
                // 光源方向
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz); 
                //漫反射
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLightDir)) ;
                
                // 反射光的方向
                fixed3 reflectDir = normalize(reflect(-worldLightDir, worldNormal));
                // 视角方向
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                //高光反射
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(reflectDir, viewDir)), _Gloss);

                // 最终颜色 = 漫反射 + 环境光 + 高光反射
                return fixed4(diffuse + ambient + specular, 1.0); 
            }

            ENDCG
        }
        
    }
    FallBack "Specular"
}
