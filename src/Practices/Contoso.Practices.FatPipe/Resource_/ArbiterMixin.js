﻿__d("ArbiterMixin", ["Arbiter"], function (a, b, c, d, e, f) { var g = b('Arbiter'), h = { _getArbiterInstance: function () { return this._arbiter || (this._arbiter = new g()); }, inform: function (i, j, k) { return this._getArbiterInstance().inform(i, j, k); }, subscribe: function (i, j, k) { return this._getArbiterInstance().subscribe(i, j, k); }, unsubscribe: function (i) { this._getArbiterInstance().unsubscribe(i); }, registerCallback: function (i, j) { this._getArbiterInstance().registerCallback(i, j); } }; e.exports = h; });