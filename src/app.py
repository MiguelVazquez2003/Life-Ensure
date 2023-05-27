from flask import Flask, request
from collections import Counter

from src.api.v1.list_api import app

# @app.route('/get_colonia/<colonia>', methods=['GET'])
# def get_colonia_accidentes(colonia):
#     colonia = colonia.upper()
#     accidentes_colonia = [row for row in colonias if row.upper() == colonia]
#     cantidad_accidentes = len(accidentes_colonia)
#     return f"La colonia {colonia} tiene {cantidad_accidentes} accidentes."

# @app.route('/colonia_mas_comun', methods=['GET'])
# def get_colonia_mas_comun():
#     return f"La colonia que más accidentes tiene es: {colonia_mas_comun}"

if __name__ == '__main__':
    app.run(debug=True)
