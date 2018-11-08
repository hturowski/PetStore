require 'mustache'
Mustache.template_file = File.dirname(__FILE__) + '/pet_store_production_template.yml'

view = Mustache.new

view[:SERVICE_NAME]         = ENV['SERVICE_NAME']
view[:BRANCH_NAME]          = ENV['BRANCH_NAME']
view[:DOCKER_IMAGE]         = ENV['DOCKER_IMAGE']
view[:DBNAME]               = ENV['DBNAME']
view[:EXTERNAL_PORT]        = ENV['EXTERNAL_PORT']

puts view.render
