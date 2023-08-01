/*
Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
For licensing, see license.txt or http://cksource.com/ckfinder/license
*/

CKFinder.customConfig = function( config )
{

	// http://docs.cksource.com/ckfinder_2.x_api/symbols/CKFinder.config.html

	// Sample configuration options:
	config.uiColor = '#BDE31E';
    config.filebrowserBrowseUrl = "/Areas/AdminCP/Data/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Areas/AdminCP/Data/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashUrl = "/Areas/AdminCP/Data/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/Areas/AdminCP/Data/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/Areas/AdminCP/Data/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Areas/AdminCP/Data/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
    config.language = 'vi';
    config.removePlugins = 'help';

};
