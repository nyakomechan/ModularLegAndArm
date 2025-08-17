# Modular Leg And Arm
VRChat向けのアバターの手、腕や足をD&Dのみで非破壊的に義手義足などのモデルに置き換えられるようにするツールです。

元のアバターと置き換えるモデルのボーンを結合するような動作をします。

手足の長さが変動した場合に生じるIKのずれ（脚が長くなった場合に地面に足が埋まる等）を自動的に補正します。  
また置き換えるモデルのメッシュがアバターのメッシュと干渉するのを防ぐため、その部分のアバターのメッシュのポリゴンを削除するよう設定できます。

Modular Avatarと互換性があります。 

置き換える側のモデルに設定したアセットをアバターに導入する場合の手順  
(https://docs.google.com/document/d/1A_kRYmQ67VcH83QUPNurIdWlq0opKzlTO_gfc-6Y7xo/edit?tab=t.0#heading=h.qdqdfu56c05p)


Unity2022.3.22f1にて動作確認

## 前提アセット

事前に以下のパッケージの導入が必要です。

* Modular Avatar (https://github.com/bdunderscore/modular-avatar)
* Avatar Optimizer (https://github.com/anatawa12/AvatarOptimizer)

## 使い方



## コンポーネント一覧

* ReplaceAvatarBone

アバタービルド時（アバターアップロード時）、指定したHumanoidBone(LowerLeg_Lなど)をこのコンポーネントをアタッチしたオブジェクトに疑似的に置き換えます。


指定したHumanoidBoneの位置、向きがアタッチしたオブジェクトの位置、向きになります。元のボーンの削除は行いません。  
また関節の位置や手足の長さが変動した場合に生じるIKのずれ（脚が長くなった場合に地面に足が埋まる、手が長くなった場合にコントローラとアバターの手の位置がずれる等）を自動的に補正します。

* RemoveMeshHelper

AvatarOptimizerのRemoveMeshByBoxの設定補助用のコンポーネントになります。  
指定したSkinnedMeshRendererのメッシュについて、設定したボックス状の範囲のポリゴンを削除します。  

衣装や義手義足側のオブジェクトにアタッチできるので、衣装、義手義足のアセットを配布する側が干渉を防ぐために体のメッシュのポリゴンを削除する範囲を指定できます。  
アバターにより体のSkinnedMeshRendererのオブジェクト名が異なるため、衣装、義手義足アセットを導入するユーザー（アセットの購入者）側で導入時にポリゴンの削除を行うオブジェクトを選択する形を想定しています。

* …説明追加予定

## サンプルプレハブ

Packages/Modular Leg And Arm/Sample/sampleLeg.prefab、sampleArm.prefabがそれぞれ義足、義手のコンポーネント設定済みのサンプルのプレハブになります。
