Shader "Custom/BlinnPhong"{
    Properties{
        _Diffuse("Diffuse Color", Color) = (1, 1, 1, 1) 
        _Specular("Specular Color", Color) = (1, 1, 1, 1)
        _Gloss("Gloss", Range(8.0, 256)) = 20 
    }
    SubShader{
        Pass {           

            Tags{"LightMode" = "ForwardBase"}

            CGPROGRAM

            #include "Lighting.cginc" 
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Diffuse; // 使用属性
            fixed4 _Specular;
            float _Gloss;

            struct a2v
            {
                float4 vertex : POSITION; 
                float3 normal : NORMAL;   
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0; 
                float3 worldPos : TEXCOORD1;
            };

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
                // 光照方向
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                //漫反射
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLightDir)) ;
                
                // 反射光的方向
                fixed3 reflectDir = normalize(reflect(-worldLightDir, worldNormal)); 
                // 视野方向
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                fixed3 halfDir = normalize(worldLightDir + viewDir);
                //高光反射
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(dot(worldNormal, halfDir), 0), _Gloss);

                // 最终颜色 = 漫反射 + 环境光 + 高光反射
                return fixed4(diffuse + ambient + specular, 1.0); 
            }

            ENDCG
        }
        
    }
    FallBack "Specular"
}
