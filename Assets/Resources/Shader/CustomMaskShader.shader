Shader "Custom/CustomMaskShader" {
Properties {
 _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
 _BlurPower ("BlurPower", Range(0,0.999)) = 0.002
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  Offset -1, -1
Program "vp" {
SubProgram "opengl " {
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
"!!ARBvp1.0
PARAM c[5] = { program.local[0],
		state.matrix.mvp };
MOV result.color, vertex.color;
MOV result.texcoord[0].xy, vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 6 instructions, 0 R-regs
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_2_0
dcl_position0 v0
dcl_texcoord0 v1
dcl_color0 v2
mov oD0, v2
mov oT0.xy, v1
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedaecmkfackfdnjoikpcimidpmmfffmfhdabaaaaaaeiacaaaaadaaaaaa
cmaaaaaajmaaaaaabaabaaaaejfdeheogiaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaafjaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaagcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaafaepfdejfeejepeoaafeeffiedepepfceeaaedepemepfcaaepfdeheo
gmaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaa
apaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaagfaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaa
feeffiedepepfceeaaedepemepfcaaklfdeieefcdaabaaaaeaaaabaaemaaaaaa
fjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaad
dcbabaaaabaaaaaafpaaaaadpcbabaaaacaaaaaaghaaaaaepccabaaaaaaaaaaa
abaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagiaaaaac
abaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaaaaaaaaa
abaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaaaaaaaaaaagbabaaa
aaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaaaaaaaaa
acaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaa
egiocaaaaaaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadgaaaaaf
dccabaaaabaaaaaaegbabaaaabaaaaaadgaaaaafpccabaaaacaaaaaaegbobaaa
acaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
Bind "vertex" Vertex
Bind "color" Color
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0_level_9_1
eefiecedebfponefkelienihfbfeflohfjhkefknabaaaaaadiadaaaaaeaaaaaa
daaaaaaabmabaaaafeacaaaameacaaaaebgpgodjoeaaaaaaoeaaaaaaaaacpopp
laaaaaaadeaaaaaaabaaceaaaaaadaaaaaaadaaaaaaaceaaabaadaaaaaaaaaaa
aeaaabaaaaaaaaaaaaaaaaaaaaacpoppbpaaaaacafaaaaiaaaaaapjabpaaaaac
afaaabiaabaaapjabpaaaaacafaaaciaacaaapjaafaaaaadaaaaapiaaaaaffja
acaaoekaaeaaaaaeaaaaapiaabaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapia
adaaoekaaaaakkjaaaaaoeiaaeaaaaaeaaaaapiaaeaaoekaaaaappjaaaaaoeia
aeaaaaaeaaaaadmaaaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeia
abaaaaacaaaaadoaabaaoejaabaaaaacabaaapoaacaaoejappppaaaafdeieefc
daabaaaaeaaaabaaemaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaad
pcbabaaaaaaaaaaafpaaaaaddcbabaaaabaaaaaafpaaaaadpcbabaaaacaaaaaa
ghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaad
pccabaaaacaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
aaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaadgaaaaaf
pccabaaaacaaaaaaegbobaaaacaaaaaadoaaaaabejfdeheogiaaaaaaadaaaaaa
aiaaaaaafaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaafjaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadadaaaagcaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaacaaaaaaapapaaaafaepfdejfeejepeoaafeeffiedepepfceeaaedep
emepfcaaepfdeheogmaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
adamaaaagfaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaafdfgfpfa
epfdejfeejepeoaafeeffiedepepfceeaaedepemepfcaakl"
}
}
Program "fp" {
SubProgram "opengl " {
SetTexture 0 [_MainTex] 2D 0
"!!ARBfp1.0
PARAM c[2] = { { -0.016000001, 0.016000001, 0.094741598, 0 },
		{ 0.118318, 0.147761 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEMP R7;
TEMP R8;
TEX R8, fragment.texcoord[0], texture[0], 2D;
ADD R2.xy, fragment.texcoord[0], c[0].wyzw;
ADD R2.zw, fragment.texcoord[0].xyxy, c[0].xywx;
ADD R3.xy, fragment.texcoord[0], c[0].ywzw;
ADD R3.zw, fragment.texcoord[0].xyxy, c[0].xyxw;
ADD R1.zw, fragment.texcoord[0].xyxy, c[0].y;
ADD R0.zw, fragment.texcoord[0].xyxy, c[0].xyyx;
ADD R1.xy, fragment.texcoord[0], c[0].x;
ADD R0.xy, fragment.texcoord[0], c[0];
TEX R7, R3.zwzw, texture[0], 2D;
TEX R6, R3, texture[0], 2D;
TEX R3, R0.zwzw, texture[0], 2D;
TEX R0, R0, texture[0], 2D;
TEX R5, R2.zwzw, texture[0], 2D;
TEX R4, R2, texture[0], 2D;
TEX R2, R1.zwzw, texture[0], 2D;
TEX R1, R1, texture[0], 2D;
MUL R2, R2, c[0].z;
MUL R1, R1, c[0].z;
MUL R0, R0, c[0].z;
ADD R0, R0, R1;
ADD R0, R0, R2;
MUL R1, R3, c[0].z;
ADD R0, R0, R1;
MUL R2, R4, c[1].x;
ADD R0, R0, R2;
MUL R1, R5, c[1].x;
ADD R0, R0, R1;
MUL R2, R6, c[1].x;
ADD R0, R0, R2;
MUL R1, R7, c[1].x;
MUL R2, R8, c[1].y;
ADD R0, R0, R1;
ADD R0, R0, R2;
MUL result.color, R0, fragment.color.primary;
END
# 35 instructions, 9 R-regs
"
}
SubProgram "d3d9 " {
SetTexture 0 [_MainTex] 2D 0
"ps_2_0
dcl_2d s0
def c0, -0.01600000, 0.01600000, 0.09474160, 0.00000000
def c1, 0.11831800, 0.14776100, 0, 0
dcl t0.xy
dcl v0
add r7.xy, t0, c0.x
add r6.xy, t0, c0.y
add r8.xy, t0, c0
mov r0.y, c0.x
mov r0.x, c0.y
add r5.xy, t0, r0
mov r0.y, c0
mov r0.x, c0.w
add r4.xy, t0, r0
mov r0.y, c0.x
mov r0.x, c0.w
add r3.xy, t0, r0
mov r0.y, c0.w
mov r0.x, c0.y
add r2.xy, t0, r0
mov r0.y, c0.w
mov r0.x, c0
add r1.xy, t0, r0
texld r0, t0, s0
texld r1, r1, s0
texld r2, r2, s0
texld r3, r3, s0
texld r4, r4, s0
texld r5, r5, s0
texld r8, r8, s0
texld r6, r6, s0
texld r7, r7, s0
mul r6, r6, c0.z
mul r7, r7, c0.z
mul r8, r8, c0.z
add_pp r7, r8, r7
add_pp r6, r7, r6
mul r5, r5, c0.z
mul r4, r4, c1.x
add_pp r5, r6, r5
add_pp r4, r5, r4
mul r3, r3, c1.x
mul r2, r2, c1.x
add_pp r3, r4, r3
add_pp r2, r3, r2
mul r1, r1, c1.x
mul r0, r0, c1.y
add_pp r1, r2, r1
add_pp r0, r1, r0
mul_pp r0, r0, v0
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
"ps_4_0
eefiecedgpcieaabbgcjbaemojpginolemihbagaabaaaaaaniaeaaaaadaaaaaa
cmaaaaaakaaaaaaaneaaaaaaejfdeheogmaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaafmaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaagfaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaedepemepfcaakl
epfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
aaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcpmadaaaaeaaaaaaa
ppaaaaaafkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaa
gcbaaaaddcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagfaaaaadpccabaaa
aaaaaaaagiaaaaacadaaaaaaaaaaaaakpcaabaaaaaaaaaaaegbebaaaabaaaaaa
aceaaaaagpbcidlmgpbciddmgpbcidlmgpbcidlmefaaaaajpcaabaaaabaaaaaa
ogakbaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaa
aaaaaaaaegaabaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaak
pcaabaaaabaaaaaaegaobaaaabaaaaaaaceaaaaaocahmcdnocahmcdnocahmcdn
ocahmcdndcaaaaampcaabaaaaaaaaaaaegaobaaaaaaaaaaaaceaaaaaocahmcdn
ocahmcdnocahmcdnocahmcdnegaobaaaabaaaaaaaaaaaaakpcaabaaaabaaaaaa
egbebaaaabaaaaaaaceaaaaagpbciddmgpbciddmgpbciddmgpbcidlmefaaaaaj
pcaabaaaacaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaaacaaaaaaaceaaaaaocahmcdn
ocahmcdnocahmcdnocahmcdnegaobaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaa
egaobaaaabaaaaaaaceaaaaaocahmcdnocahmcdnocahmcdnocahmcdnegaobaaa
aaaaaaaaaaaaaaakpcaabaaaabaaaaaaegbebaaaabaaaaaaaceaaaaaaaaaaaaa
gpbciddmaaaaaaaagpbcidlmefaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaa
egaobaaaacaaaaaaaceaaaaalffapcdnlffapcdnlffapcdnlffapcdnegaobaaa
aaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaaabaaaaaaaceaaaaalffapcdn
lffapcdnlffapcdnlffapcdnegaobaaaaaaaaaaaaaaaaaakpcaabaaaabaaaaaa
egbebaaaabaaaaaaaceaaaaagpbciddmaaaaaaaagpbcidlmaaaaaaaaefaaaaaj
pcaabaaaacaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaaacaaaaaaaceaaaaalffapcdn
lffapcdnlffapcdnlffapcdnegaobaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaa
egaobaaaabaaaaaaaceaaaaalffapcdnlffapcdnlffapcdnlffapcdnegaobaaa
aaaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaaabaaaaaaaceaaaaa
kjeobhdokjeobhdokjeobhdokjeobhdoegaobaaaaaaaaaaadiaaaaahpccabaaa
aaaaaaaaegaobaaaaaaaaaaaegbobaaaacaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
SetTexture 0 [_MainTex] 2D 0
"ps_4_0_level_9_1
eefiecedbimalfplggipacfbdeeehbcgejmhffpfabaaaaaafmahaaaaaeaaaaaa
daaaaaaalaacaaaaleagaaaaciahaaaaebgpgodjhiacaaaahiacaaaaaaacpppp
faacaaaaciaaaaaaaaaaciaaaaaaciaaaaaaciaaabaaceaaaaaaciaaaaaaaaaa
aaacppppfbaaaaafaaaaapkagpbcidlmgpbciddmocahmcdnlffapcdnfbaaaaaf
abaaapkaaaaaaaaagpbciddmgpbcidlmaaaaaaaafbaaaaafacaaapkagpbciddm
aaaaaaaakjeobhdoaaaaaaaabpaaaaacaaaaaaiaaaaacdlabpaaaaacaaaaaaia
abaacplabpaaaaacaaaaaajaaaaiapkaacaaaaadaaaaadiaaaaaoelaaaaaaaka
acaaaaadabaaadiaaaaaoelaaaaaoekaacaaaaadacaaadiaaaaaoelaaaaaffka
acaaaaadadaaadiaaaaaoelaaaaaoekbacaaaaadaeaaadiaaaaaoelaabaaoeka
acaaaaadafaaadiaaaaaoelaabaablkaacaaaaadagaaadiaaaaaoelaacaaoeka
acaaaaadahaaadiaaaaaoelaabaanckaecaaaaadaaaaapiaaaaaoeiaaaaioeka
ecaaaaadabaaapiaabaaoeiaaaaioekaecaaaaadacaaapiaacaaoeiaaaaioeka
ecaaaaadadaaapiaadaaoeiaaaaioekaecaaaaadaeaaapiaaeaaoeiaaaaioeka
ecaaaaadafaaapiaafaaoeiaaaaioekaecaaaaadagaaapiaagaaoeiaaaaioeka
ecaaaaadahaaapiaahaaoeiaaaaioekaecaaaaadaiaaapiaaaaaoelaaaaioeka
afaaaaadaaaacpiaaaaaoeiaaaaakkkaaeaaaaaeaaaacpiaabaaoeiaaaaakkka
aaaaoeiaaeaaaaaeaaaacpiaacaaoeiaaaaakkkaaaaaoeiaaeaaaaaeaaaacpia
adaaoeiaaaaakkkaaaaaoeiaaeaaaaaeaaaacpiaaeaaoeiaaaaappkaaaaaoeia
aeaaaaaeaaaacpiaafaaoeiaaaaappkaaaaaoeiaaeaaaaaeaaaacpiaagaaoeia
aaaappkaaaaaoeiaaeaaaaaeaaaacpiaahaaoeiaaaaappkaaaaaoeiaaeaaaaae
aaaacpiaaiaaoeiaacaakkkaaaaaoeiaafaaaaadaaaacpiaaaaaoeiaabaaoela
abaaaaacaaaicpiaaaaaoeiappppaaaafdeieefcpmadaaaaeaaaaaaappaaaaaa
fkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaad
dcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagfaaaaadpccabaaaaaaaaaaa
giaaaaacadaaaaaaaaaaaaakpcaabaaaaaaaaaaaegbebaaaabaaaaaaaceaaaaa
gpbcidlmgpbciddmgpbcidlmgpbcidlmefaaaaajpcaabaaaabaaaaaaogakbaaa
aaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaaaaaaaaa
egaabaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaakpcaabaaa
abaaaaaaegaobaaaabaaaaaaaceaaaaaocahmcdnocahmcdnocahmcdnocahmcdn
dcaaaaampcaabaaaaaaaaaaaegaobaaaaaaaaaaaaceaaaaaocahmcdnocahmcdn
ocahmcdnocahmcdnegaobaaaabaaaaaaaaaaaaakpcaabaaaabaaaaaaegbebaaa
abaaaaaaaceaaaaagpbciddmgpbciddmgpbciddmgpbcidlmefaaaaajpcaabaaa
acaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
dcaaaaampcaabaaaaaaaaaaaegaobaaaacaaaaaaaceaaaaaocahmcdnocahmcdn
ocahmcdnocahmcdnegaobaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaa
abaaaaaaaceaaaaaocahmcdnocahmcdnocahmcdnocahmcdnegaobaaaaaaaaaaa
aaaaaaakpcaabaaaabaaaaaaegbebaaaabaaaaaaaceaaaaaaaaaaaaagpbciddm
aaaaaaaagpbcidlmefaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaa
acaaaaaaaceaaaaalffapcdnlffapcdnlffapcdnlffapcdnegaobaaaaaaaaaaa
dcaaaaampcaabaaaaaaaaaaaegaobaaaabaaaaaaaceaaaaalffapcdnlffapcdn
lffapcdnlffapcdnegaobaaaaaaaaaaaaaaaaaakpcaabaaaabaaaaaaegbebaaa
abaaaaaaaceaaaaagpbciddmaaaaaaaagpbcidlmaaaaaaaaefaaaaajpcaabaaa
acaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
dcaaaaampcaabaaaaaaaaaaaegaobaaaacaaaaaaaceaaaaalffapcdnlffapcdn
lffapcdnlffapcdnegaobaaaaaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaa
abaaaaaaaceaaaaalffapcdnlffapcdnlffapcdnlffapcdnegaobaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadcaaaaampcaabaaaaaaaaaaaegaobaaaabaaaaaaaceaaaaakjeobhdo
kjeobhdokjeobhdokjeobhdoegaobaaaaaaaaaaadiaaaaahpccabaaaaaaaaaaa
egaobaaaaaaaaaaaegbobaaaacaaaaaadoaaaaabejfdeheogmaaaaaaadaaaaaa
aiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaafmaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadadaaaagfaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaacaaaaaaapapaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaedepemepfcaaklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl"
}
}
 }
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  AlphaTest Greater 0.01
  ColorMask RGB
  ColorMaterial AmbientAndDiffuse
  Offset -1, -1
  SetTexture [_MainTex] { combine texture * primary }
 }
}
}