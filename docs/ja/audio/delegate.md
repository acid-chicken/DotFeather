# DelegateAudioSource

`DelegateAudioSource` クラスを用いることで、コールバック関数を用いて生成した波形
を再生できます。PCM 音源レベルの低レイヤーな音声処理を行えます。

DelegateAudioSource クラスのコンストラクタには、実際に再生される波形を処理するた
めのコールバック関数を指定します。例を示します。

```cs
var player = new AudioPlayer();
var source = new DelegateAudioSource((sampleCount, _) =>
{
    var s = (short)(MathF.Sin(2 * MathF.PI * sampleCount * 440 / 44100) * 10000);
    return (s, s);
});

player.Play(source);
```

この例では、コールバック関数内で 440Hz のサイン波を生成しています。コールバック
の第１引数はサンプル位置、第２引数はループの開始位置（ループをしない場合は
null）が入り、戻り値は `(short, short)` タプル型あるいは `null` です。`null` を
返すことで、演奏の停止を明示します。

実例は[こちら](/demo/Scenes/Examples/audio/DelegateExampleScene.cs)を御覧くださ
い。
