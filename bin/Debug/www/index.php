<?php
require_once ('./model/PageLoader.php');
require_once('./model/ArticleLoader.php');
require_once('./model/commonPages.php');
require_once('./model/Authorization.php');


session_start();

$pageLoader = new PageLoader();

if (empty($_GET['page'])||!(array_key_exists($_GET['page'],$commonPages)))
      $pageID = 'home';
    else
      $pageID = $_GET['page'];

  $pageID = $pageLoader->LimitAccess($pageID);
  $pageLoader->LoadPage($commonPages[$pageID]);


