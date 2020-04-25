# ![DotFeather](docs/logo.svg)

[![Build status](https://img.shields.io/appveyor/ci/xeltica/dotfeather.svg?style=for-the-badge)][ci]
[![GitHub Releases](https://img.shields.io/github/release-pre/xeltica/DotFeather.svg?style=for-the-badge)][releases]
[![Nuget](https://img.shields.io/nuget/vpre/DotFeather.svg?style=for-the-badge)](https://www.nuget.org/packages/DotFeather/)
[![Discord](https://img.shields.io/discord/572071627991416851?label=discord&style=for-the-badge)](https://discord.gg/e2TCF4m)

DotFeather (ドットフェザー)は、 C# と .NET Standard 2.1 のための、軽量で汎用的な
2D ゲームエンジンです。

[English](README.md) ・ 日本語

## ビルドの仕方

```shell
git clone https://github.com/xeltica/DotFeather.git
cd DotFeather
nuget restore
dotnet build
```

## サポートされるプラットフォー�

-   Windows
-   macOS
-   GNU/Linux

## 機能

-   軽快な処理 - 10000 スプライトを 60fps で表示可能 [<sup>\*1</sup>](#f1)
-   2D に特化したグラフィックシステム - スプライト - 画面上へのテクスチャ表示 -
    タイルマップ - テクスチャを敷き詰めたマップ表示 - グラフィック - 線分や矩形
    を 1 ピクセル単位で描画 - コンテナー - 描画要素を格納できるオブジェクト - テ
    キスト - 文字列を描画できるオブジェクト - 9 スライススプライト - テクスチャ
    を 9 分割して、矩形状のテクスチャ − をスムーズに引き伸ばせる特殊なスプライト
-   キーボード入力
-   マウス入力
-   スクリーンショット撮影機能
-   動画作成を支援するための、画面を連番画像でキャプチャーする機能
-   シーン遷移機能
-   音楽再生
-   効果音再生
-   返り値の取得やエラーハンドリングを備えた Unity ライクなコルーチンシステ�
-   高い拡張性 - 独自の描画機能の追加 - オーディオ機能の拡張

---

<p id="f1">1: ユーザーのコンピュータースペックに依存します。</p>

## ドキュメント

[ドキュメント](https://xeltica.github.io/DotFeather/docs/ja/)

## コントリビュート

[コントリビュートの手引き](CONTRIBUTING-ja.md) をご確認ください。

[![GitHub issues](https://img.shields.io/github/issues/xeltica/dotfeather.svg?style=for-the-badge)][issues]
[![GitHub pull requests](https://img.shields.io/github/issues-pr/xeltica/dotfeather.svg?style=for-the-badge)][pulls]

## 寄付

寄付をしたい！という方、大変ありがとうございます
。[このページ](//xeltica.work/donation.html)に私への寄付手段がまとまっているので
、ご確認ください。

もしくは Patreon で私のパトロンになってください！

[![become_a_patron](https://c5.patreon.com/external/logo/become_a_patron_button@2x.png)](https://patreon.com/xeltica)

## ライセンス

[![License](https://img.shields.io/github/license/xeltica/dotfeather.svg?style=for-the-badge)](LICENSE)

[ci]: https://ci.appveyor.com/project/Xeltica/dotfeather
[issues]: //github.com/xeltica/dotfeather/issues
[pulls]: //github.com/xeltica/dotfeather/pulls
[releases]: //github.com/xeltica/dotfeather/releases
