# TeleportSetter

Dynamic Teleport Setter

## 概要

テレポート先をインデックスリストで動的に管理することが出来るワールドアセットです。

## インストール

### VCCによる方法

1. https://vpm.narazaka.net/ から「Add to VCC」ボタンを押してリポジトリをVCCにインストールします。
2. VCCでSettings→Packages→Installed Repositoriesの一覧中で「Narazaka VPM Listing」にチェックが付いていることを確認します。
3. アバタープロジェクトの「Manage Project」から「TeleportSetter」をインストールします。

## 使い方

1. 「TeleportSetter」プレハブをシーン上に1つ置きます。

2. テレポート元Interact対象のオブジェクトに「Add Component」から「Teleporter」を追加します。

3. テレポート元のTeleporterとテレポート先のTransformをTeleportSetterに指定します。

4. 初期テレポート先インデックスを設定します。

5. TeleportSetter.TeleportTargetIndexesをプログラムから動的に制御することでテレポート先を変更することが可能です。

## 更新履歴

- 1.0.0
  - リリース

## License

[Zlib License](LICENSE.txt)
