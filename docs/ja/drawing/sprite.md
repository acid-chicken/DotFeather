# スプライト

スプライトは、画面上の自由な位置にテクスチャを描画するためのオブジェクトです。ゲ
ームキャラや弾丸の表示などに利用できます。

## テクスチャ

テクスチャとは、 DotFeather で読み込まれた画像データを指します。

テクスチャは `Texture2D` 構造体で表現され、次に示すとおり `Texture2D` 構造体の静
的メソッドを呼ぶことで読み込めます。

```cs
Texture2D texture = Texture2D.LoadFrom("./assets/title.png");
```

### テクスチャの分割読み込み

スプライトシートという、一枚の画像にアニメーション用の絵が格子状に配置されている
ような画像を簡単に読み込めるよう、 DotFeather には、テクスチャを分割して読み込む
機能があります。

```cs
Texture2D[] textures = Texture2D.LoadAndSplitFrom("./assets/zombie.png", 16, 2, new Size(16, 16));
```

このプログラムは、 ./assets/zombie.png にあるファイルを読み込み、16x16 のテクス
チャとして、横 16 個、縦 2 個、合計 32 個切り出します。 DX ライブラリの
「LoadDivGraph()」関数などに該当します。

## スプライト

テクスチャを読み込んだら、スプライトのインスタンスを生成して表示させられます。

```cs
Sprite title = new Sprite(texture, 0, 32);
Sprite zombie = new Sprite(textures[0], 64, 16);
Root.Add(sprite);
```

また、ファイル名を指定して直接スプライトを生成することも出来ます。

```cs
Sprite sprite = Sprite.LoadFrom("./assets/skeleton.png");
```

## アニメーションするスプライト

`AnimatingSprite` クラスは、分割読み込みしたテクスチャの配列を読み込んで、自動的
にテクスチャアニメーションを行います。

次のようにして、`AnimatingSprite` を実際に使ってみましょう。

```cs
var zombieWalking = new AnimatingSprite(textures, -1, 4);
Root.Add(zombieWalking);

// 忘れずに
zombieWalking.StartAnimating();
```

`AnimatingSprite` クラスのコンストラクターは、第 1 引数にテクスチャ配列、第 2 引
数にループ回数、そして第 3 引数に時間を指定します。

ループ回数は、1 以上であればその回数ループします。0 であればループしません。そし
て、-1 であれば無限ループします。

時間は、次のテクスチャに切り替わるまでのフレーム時間です。例えば、5 であれば、1
枚のテクスチャが 5 フレーム表示されます。

`AnimatingSprite` は、初期化後に自動的にはアニメーションを行わないので、かならず
`StartWalking` メソッドを呼び出してください。

次: [タイルマップ](tilemap.md)
