module.exports = {
  "configureWebpack": {
    "devServer": {
      "proxy": "https://localhost:5001"
    }
  },
  "transpileDependencies": [
    "vuetify"
  ]
}