# オーディオ

DotFeather には、音声をファイルなどのソースから読み込み、再生する機能があります
。

DotFeather が扱える音声データは、 **オーディオソース** として表現されます。 オー
ディオソースは`IAudioSource` インターフェイスを継承したクラスです。

標準で、 Microsoft WAV フォーマットおよび Ogg Vorbis フォーマットを読み込めるオ
ーディオソースおよび、コールバック関数を用いて波形をプログラマブルに生成でき
る[DelegateAudioSource](audio/delegate.md)が組み込まれています。また、
[オーディオソースを自作する](plugin/audiosource.md)ことによって、あらゆるオーデ
ィオファイルに対応できます。

オーディオソースは、 `AudioPlayer` クラスを用いることで再生できます。

次: [ファイルの読み込み](audio/load.md)
