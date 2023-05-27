from flask import Flask, Blueprint

# from src.api.v1.directions.api import directions_dumy

flaks_app = Flask(__name__)
flaks_app.config.from_object(flask_config)

api_blueprint = Blueprint('api', __name__, url_prefix='/')

# api_blueprint.register_blueprint(directions_dumy)

app = init_app(flaks_app)