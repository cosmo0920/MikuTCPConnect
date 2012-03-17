# -*- coding: utf-8 -*-
require 'socket'
Plugin.create :socket_tweet_server do
  #複数回ツイートを送信するために複数ソケット通信対応サーバーとする
  @thread = SerialThreadGroup.new
  @thread.new{
    server = TCPServer.open(3939)
    while true
      Thread.start(server.accept) do |client|
        begin                         
          client.puts(Time.now.ctime)
          #受け取った内容を呟くよ
          Post.primary_service.update(:message => "#{client.gets}")
        rescue IOError
          print "IOError."
        end
        client.close                 
      end
    end
  }
end
