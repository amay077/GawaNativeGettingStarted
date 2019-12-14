Xamarin.Forms でガワネイティブアプリを作るときのテンプレートプロジェクトを作る１

今年は専ら Angular で Webアプリを作ったり、ガワネイティブアプリを作ったりしています。

## 概要

最近は、モバイルネイティブアプリよりも SPA/PWA、そしてそれを利用したガワネイティブアプリを推奨している私ですが、ガワネイティブアプリを作る時の「ガワ」には Xamarin(Xamarin.Forms) を採用しています。

ガワネイティブアプリは、Webアプリがネイティブの機能を欲するから採用されるわけで、それを制御するためにWebアプリとネイティブ機能の相互通信が必要になります。

また、「ガワ」は ``WebView`` なわけですが、それがアプリとして自然に振る舞うために、いくつかの「設定」をしてあげる必要があります。

この記事は、そのような「Xamarin(.Forms) でガワネイティブアプリを作るときのリファレンス」になればよいなと思って書きます。

尚、~~Advent Calendar の締め切りに間に合わせるために~~ 意外と情報量が多かったので、前編と後編に分けます。
今回は前編です。

## 目次

1. 日本語入力時の画面高さの調整
2. アプリ情報の Web 側への引き渡し
3. ステータスバー、あるいは SafeArea(ノッチ部)の色
3. ``<input type="xxx">`` への対応
4. Back ボタンハンドリングの Web 側への移譲


## 参考

* [iOSでWebViewを開発する際に気をつけるべき9のこと - Qiita](https://qiita.com/nikadon/items/1a38761012d530db112a)
* [(Xamarin)Webサイトのオフライン閲覧アプリを作るときの注意 - Qiita](https://qiita.com/khosokawa/items/e319c5ca65e3de9c81db)
* [WebViewの<input type="file">でカメラ、画像からデータを取得する [Android]](https://www.petitmonte.com/java/android_webview_camera.html)
* [html5 カメラ起動 capture属性について - Qiita](https://qiita.com/RIKIgigasu/items/0c710fa608dd5a7a45cb)
* [Materialize](https://materializecss.com/)
* [SafeAreaInsets For Xamarin.Forms In iOS - Xamarin Help](https://xamarinhelp.com/safeareainsets-xamarin-forms-ios/)